using Xunit;
using analyzeJSON;
using Newtonsoft.Json.Linq;

namespace analyzeJSONTests
{
    public class AnalyzeStructureUnitTests
    {

        [Fact]
        public void AnalyzeToken_AllGood()
        {
            dynamic token = new JObject();
            token.text = "something";

            var leaf = token["text"];
            var node = new JProperty("prop", JToken.Parse(@"{ ""text"" : ""lorem"", ""text2"" : ""ipsum"" }"));

            var analyze = new AnalyzeStructure();
            analyze.AnalyzeToken(leaf);
            analyze.AnalyzeToken(node);

            var result = analyze.Result;
            _ = Assert.Single(result.Nodes);
            _ = Assert.Single(result.Leafs);
        }
    }
}
