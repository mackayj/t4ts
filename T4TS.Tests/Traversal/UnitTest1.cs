using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnvDTE;
using Moq;
using EnvDTE80;
using System.Reflection;

namespace T4TS.Tests.Traversal
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var moqDte = new Mock<DTE>();
            var moqSolution = new Mock<Solution>();
            var moqProject = new Mock<Project>();
            var moqProjects = new Mock<Projects>();
            var moqProjectItem = new Mock<ProjectItem>();
            var moqProjectItems = new Mock<ProjectItems>();

            var moqFileCodeModel = new Mock<FileCodeModel>();
            var moqProjCodeElements = new Mock<CodeElements>();
            var moqCodeNamespace = new Mock<CodeNamespace>();
            var moqMembers = new Mock<CodeElements>();
            var moqMember = new Mock<CodeClass>();

            var moqAttributes = new Mock<CodeElements>();
            var moqAttribute = new Mock<CodeAttribute>();
            var moqAttributeChildren = new Mock<CodeElements>();
            var moqAttributeChild = new Mock<CodeAttributeArgument>();


            moqDte.SetupGet(x => x.Solution).Returns(moqSolution.Object);
            
            moqSolution.SetupGet(x => x.Projects).Returns(moqProjects.Object);
            
            moqProject.SetupGet(x => x.ProjectItems).Returns(moqProjectItems.Object);
            moqProjects.Setup(x => x.GetEnumerator()).Returns(new[] { moqProject.Object }.GetEnumerator());

            moqProjectItem.SetupProperty(x => x.Name, "Foobar");
            moqProjectItem.SetupGet(x => x.FileCodeModel).Returns(moqFileCodeModel.Object);
            moqProjectItem.SetupGet(x => x.ProjectItems).Returns((ProjectItems)null);
            moqProjectItems.Setup(x => x.GetEnumerator()).Returns(new[] { moqProjectItem.Object }.GetEnumerator());

            var namespaces = new List<CodeNamespace>
            { 
                moqCodeNamespace.Object
            };

            moqFileCodeModel.SetupGet(x => x.CodeElements).Returns(moqProjCodeElements.Object);
            moqProjCodeElements.Setup(x => x.GetEnumerator()).Returns(() => namespaces.GetEnumerator());

            moqCodeNamespace.SetupGet(x => x.Members).Returns(moqMembers.Object);
            moqMembers.Setup(x => x.GetEnumerator()).Returns(new[] { moqMember.Object }.GetEnumerator());
            moqMember.SetupGet(x => x.Attributes).Returns(moqAttributes.Object);
            moqMember.SetupGet(x => x.Name).Returns("MoqClass");
            moqMember.SetupGet(x => x.FullName).Returns("Tests.MoqClass");

            moqAttributes.Setup(x => x.GetEnumerator()).Returns(new[] { moqAttribute.Object }.GetEnumerator());
            moqAttribute.SetupGet(x => x.FullName).Returns("T4TS.TypeScriptInterfaceAttribute");
            moqAttribute.SetupGet(x => x.Children).Returns(moqAttributeChildren.Object);

            moqAttributeChildren.Setup(x => x.GetEnumerator()).Returns(new[] { moqAttributeChild.As<CodeElement>().Object }.GetEnumerator());
            moqAttributeChild.SetupGet(x => x.Value).Returns("\"Test\"");
            moqAttributeChild.SetupGet(x => x.Name).Returns("FoobarProp");


            var codeTraverser = new CodeTraverser(moqSolution.Object, new Settings());
            var interfaces = codeTraverser.GetAllInterfaces();
        }
    }
}
