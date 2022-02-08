using Xunit;
using analyzeJSON;
using System.Collections.Generic;

namespace analyzeJSONTests
{
    public class ExtractTextIntegrationTests
    {
        private static readonly string testFile = @"testFiles/complex.json";

        [Fact]
        public void ExtractText_withTraverse()
        {
            var test = new AnalyzeJSON(testFile);

            var keys = new Dictionary<string, bool>
            {
                ["name"] = true,
                ["location"] = false
            };

            var extract = new ExtractText(keys);
            var status = test.Traverse((token) => extract.Extract(token));
            Assert.True(status.Success);
            Assert.Empty(status.Message);

            Assert.Equal(2, extract.Result.Count);
            Assert.Equal(21, extract.Result["name"].Count);
            Assert.Equal(15, extract.Result["location"].Count);
        }
    }
}
