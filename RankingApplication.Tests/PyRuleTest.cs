using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IronPython.Runtime.Exceptions;
using Microsoft.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankerApplication;
using Moq;

namespace RankingApplication.Tests
{
    [TestClass]
    public class PyRuleTest
    {
        [TestMethod]
        public void Running_Rule_With_Correct_Syntax_Does_Not_Throw_An_Exception()
        {
            var snippet = new Snippet();
            var ruleCode = new StringBuilder();
            ruleCode.AppendLine("def run(snippet):");
            ruleCode.AppendLine("  return 100;");

            var rule = new PyRule() {Code = ruleCode.ToString(), Weight = 1};
            rule.Rank(snippet);
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxErrorException))]
        public void Running_Rule_With_Incorrect_Syntax_Throws_An_Exception()
        {
            var snippet = new Snippet();
            var ruleCode = new StringBuilder();
            ruleCode.AppendLine("de run(snippet):");
            ruleCode.AppendLine("  return 100;");

            var rule = new PyRule() { Code = ruleCode.ToString(), Weight = 1 };
            rule.Rank(snippet);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Rule_Script_Does_Not_Contain_Run_Function()
        {
            var snippet = new Snippet();
            var ruleCode = new StringBuilder();
            ruleCode.AppendLine("def notrun(snippet):");
            ruleCode.AppendLine("  return 100;");

            var rule = new PyRule() { Code = ruleCode.ToString(), Weight = 1 };
            rule.Rank(snippet);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeErrorException))]
        public void Rule_Script_Contains_Run_Which_Is_Not_A_Function()
        {
            var snippet = new Snippet();
            var ruleCode = new StringBuilder();
            ruleCode.AppendLine("run = 100");

            var rule = new PyRule() { Code = ruleCode.ToString(), Weight = 1 };
            rule.Rank(snippet);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Rule_Script_Returns_A_Value_Of_Incompatible_Type()
        {
            var snippet = new Snippet();
            var ruleCode = new StringBuilder();
            ruleCode.AppendLine("def run(snippet):");
            ruleCode.AppendLine("  pass");

            var rule = new PyRule() { Code = ruleCode.ToString(), Weight = 1 };
            rule.Rank(snippet);
        }
    }
}
