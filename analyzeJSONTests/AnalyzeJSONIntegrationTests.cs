using analyzeJSON;
using Xunit;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSONTests
{
    public class AnalyzeJSONIntegrationTests
    {
        private static readonly string testFile = @"testFiles/complex.json";

        [Fact]
        public void AnalyzeJSON_AllGood()
        {
            _ = new AnalyzeJSON(testFile);
        }

        [Fact]
        public void TwoActionsAtOnce()
        {
            var test = new AnalyzeJSON(testFile);
            var analyze = new AnalyzeStructure();
            var stats = new Statistics();

            var actions = new List<Action<JToken>> {
                            analyze.AnalyzeToken,
                            stats.RunStatistics
                        };
            var status = test.Traverse(actions);
            Assert.True(status.Success);
            Assert.Empty(status.Message);

            var result = analyze.Result;
            Assert.Equal(10, result.Nodes.Count);
            Assert.Equal(11, result.Leafs.Count);

            Assert.Equal(11, stats.Result.NodeCounts.Count);
            Assert.Equal(168, stats.Result.TotalWordCount);
        }
    }
}
