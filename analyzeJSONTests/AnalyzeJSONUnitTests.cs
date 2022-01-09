using System;
using System.Collections.Generic;
using analyzeJSON;
using Xunit;

namespace analyzeJSONTests
{
    public record TestCase(string Input, string ExpectedOutput);

    public class AnalyzeJSONUnitTests
    {
        [Fact]
        public void AnalyzeJSON_NullPath()
        {
            Assert.Throws<ArgumentNullException>(() => new AnalyzeJSON(null));
        }

        [Fact]
        public void GetNameFromPath_NullTokenPath()
        {
            Assert.Empty(AnalyzeJSON.GetNameFromPath(null));
        }

        [Fact]
        public void GetNameFromPath_EmptyTokenPath()
        {
            Assert.Empty(AnalyzeJSON.GetNameFromPath(string.Empty));
        }

        [Fact]
        public void GetNameFromPath_DifferentCases()
        {
            var testCases = new List<TestCase>
            {
                new TestCase("abc", "abc"),
                new TestCase("abc.def", "def"),
                new TestCase("abc.", string.Empty),
                new TestCase("abc.def.", string.Empty),

            };

            foreach (var testCase in testCases)
                Assert.Equal(testCase.ExpectedOutput, AnalyzeJSON.GetNameFromPath(testCase.Input));
        }
    }
}
