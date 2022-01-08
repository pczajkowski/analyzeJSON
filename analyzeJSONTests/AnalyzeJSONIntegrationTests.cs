using analyzeJSON;
using Xunit;

namespace analyzeJSONTests
{
    public class AnalyzeJSONIntegrationTests
    {
        private static readonly string testFile = @"testFiles/complex.json";

        [Fact]
        public void AnalyzeJSON_AllGood()
        {
            var test = new AnalyzeJSON(testFile);
        }
    }
}
