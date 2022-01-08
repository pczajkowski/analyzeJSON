using Xunit;
using analyzeJSON;

namespace analyzeJSONTests
{
    public class AnalyzeStructureIntegrationTests
    {
        private static readonly string testFile = @"testFiles/complex.json";

        [Fact]
        public void AnalyzeToken_withTraverse()
        {
            var test = new AnalyzeJSON(testFile);
            var analyze = new AnalyzeStructure();
            test.Traverse((token) => analyze.AnalyzeToken(token));

            var result = analyze.Result;
            Assert.Equal(9, result.Nodes.Count);
            Assert.Equal(9, result.Leafs.Count);
        }
    }
}
