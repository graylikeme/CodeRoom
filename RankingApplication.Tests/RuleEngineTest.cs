using RankerApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace RankingApplication.Tests
{
    
    
    /// <summary>
    ///This is a test class for RuleEngineTest and is intended
    ///to contain all RuleEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RuleEngineTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Rank
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Rank_RuleEngineGotNoRules_ThorwsException()
        {
            var rules = new IRule[] {}; // TODO: Initialize to an appropriate value
            var target = new RuleEngine(rules); // TODO: Initialize to an appropriate value
            Snippet snippet = null; // TODO: Initialize to an appropriate value
            target.Rank(snippet);
            
        }

        [TestMethod()]
        public void Rank_RuleEngineGotOneRule_ShouldReturnMultiplicationOfSpecifiedRankAndWeight()
        {
            int rank = 42;
            double weight = 3;

            var ruleRank = new Mock<IRule>();
            ruleRank.Setup(m => m.Rank(It.IsAny<Snippet>())).Returns(rank);
            ruleRank.SetupGet(m => m.Weight).Returns(weight);

            var rules = new IRule[] { ruleRank.Object }; 
            var target = new RuleEngine(rules); 
            Snippet snippet = null;

            int actual = target.Rank(snippet);

            Assert.AreEqual(rank * weight, actual);
        }

        [TestMethod]
        public void Rank_RuleEngineGotTwoRules_ShouldReturnAggregatedMultiplicationOfSpecifiedRankAndWeight()
        {
            int rank1 = 42;
            double weight1 = 3;

            int rank2 = 10;
            double weight2 = 30;

            double expectedValue = rank1*weight1 + rank2*weight2;

            Mock<IRule> ruleRank1 = GetRuleRank(rank1, weight1);
            Mock<IRule> ruleRank2 = GetRuleRank(rank2, weight2);

            var rules = new[] { ruleRank1.Object, ruleRank2.Object };
            var target = new RuleEngine(rules);

            Snippet snippet = null;
            int actual = target.Rank(snippet);

            Assert.AreEqual(expectedValue, actual);
        }

        private static Mock<IRule> GetRuleRank(int rank, double weight)
        {
            var ruleRank = new Mock<IRule>();
            ruleRank.Setup(m => m.Rank(It.IsAny<Snippet>())).Returns(rank);
            ruleRank.SetupGet(m => m.Weight).Returns(weight);
            return ruleRank;
        }
    }
}
