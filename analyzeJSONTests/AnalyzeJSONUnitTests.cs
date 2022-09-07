using System;
using System.Collections.Generic;
using analyzeJSON;
using Newtonsoft.Json.Linq;
using Xunit;

namespace analyzeJSONTests
{
    public record TestCase(string Input, string ExpectedOutput);

    public class AnalyzeJSONUnitTests
    {
        [Fact]
        public void Traverse_EmptyJObject()
        {
            var test = new AnalyzeJSON(new JObject());
            var result = test.Traverse((token) => Console.WriteLine(token));
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public void Traverse_NullAction()
        {
            var jObject = new JObject
            {
                { "test", new JObject() }
            };
            var test = new AnalyzeJSON(jObject);

            var result = test.Traverse(action: null);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public void Traverse_AllGood()
        {
            var jObject = new JObject
            {
                { "test", new JObject() }
            };
            var test = new AnalyzeJSON(jObject);

            var result = test.Traverse((token) => Console.WriteLine(token));
            Assert.True(result.Success);
            Assert.Empty(result.Message);
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
                new TestCase("abc.def.ghi[0]", "ghi"),

            };

            foreach (var testCase in testCases)
                Assert.Equal(testCase.ExpectedOutput, AnalyzeJSON.GetNameFromPath(testCase.Input));
        }

			[Fact]
        public void Traverse_EmptyJObject_WithActions()
        {
            var test = new AnalyzeJSON(new JObject());
            var result = test.Traverse(new List<Action<JToken>>{ (token) => Console.WriteLine(token) });
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public void Traverse_NullActions()
        {
            var jObject = new JObject
            {
                { "test", new JObject() }
            };
            var test = new AnalyzeJSON(jObject);

            var result = test.Traverse(actions: null);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
        }

				[Fact]
        public void Traverse_EmptyActions()
        {
            var jObject = new JObject
            {
                { "test", new JObject() }
            };
            var test = new AnalyzeJSON(jObject);

            var result = test.Traverse(new List<Action<JToken>>());
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
        }
    }
}
