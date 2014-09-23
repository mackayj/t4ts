using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnvDTE;
using Moq;
using EnvDTE80;
using System.Reflection;
using T4TS.Tests.Utils;
using T4TS.Tests.Models;
using T4TS.Example.Models;

namespace T4TS.Tests.Traversal
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var solution = DTETransformer.BuildDteSolution(
                typeof(LocalModel),
                typeof(ModelFromDifferentProject)
            );

            var codeTraverser = new CodeTraverser(solution, new Settings());
            var interfaces = codeTraverser.GetAllInterfaces();
        }
    }
}
