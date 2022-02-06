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
            var status = test.Traverse((token) => analyze.AnalyzeToken(token));
            Assert.True(status.Success);
            Assert.Empty(status.Message);

            var result = analyze.Result;
            Assert.Equal(9, result.Nodes.Count);
            Assert.Equal(10, result.Leafs.Count);
        }
    }
}
