using Xunit;
using analyzeJSON;
using Newtonsoft.Json.Linq;

namespace analyzeJSONTests
{
    public class StatisticsUnitTests
    {
        [Fact]
        public void RunStatistics_AllGood()
        {
            dynamic token = new JObject();
            token.one = "Lorem ipsum dolor";
            token.two = "Lorem      ipsum dolor";
            token.three = "Lorem\nipsum dolor";

            var stats = new Statistics();
            stats.RunStatistics(token.one);
            stats.RunStatistics(token.two);
            stats.RunStatistics(token.three);

            Assert.Equal(3, stats.Result.NodeCounts.Count);
            Assert.Equal(9, stats.Result.TotalWordCount);
        }
    }
}
