using Xunit;
using analyzeJSON;

namespace analyzeJSONTests
{
    public class StatisticsIntegrationTests
    {
        private static readonly string testFile = @"testFiles/complex.json";

        [Fact]
        public void RunStatistics_withTraverse()
        {
            var test = new AnalyzeJSON(testFile);
            var stats = new Statistics();

            test.Traverse((token) => stats.RunStatistics(token));
            Assert.Equal(10, stats.Result.NodeCounts.Count);
            Assert.Equal(165, stats.Result.TotalWordCount);
        }
    }
}
