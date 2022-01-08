using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public record AnalysisResult(Dictionary<string, int> Nodes, Dictionary<string, int> Leafs);

    public class AnalyzeStructure
    {
        private Dictionary<string, int> nodes = new Dictionary<string, int>();
        private Dictionary<string, int> leafs = new Dictionary<string, int>();

        public AnalyzeStructure()
        {
        }

        public void AnalyzeToken(JToken token)
        {
            if (token.Type == JTokenType.Object || token.Type == JTokenType.Array)
                return;

            var tokenName = AnalyzeJSON.GetNameFromPath(token.Path);

            if (token.HasValues)
            {
                if (token.First.Equals(token.Last) &&
                    token.First.Type != JTokenType.Array && token.First.Type != JTokenType.Property && token.First.Type != JTokenType.Object)
                {
                    return;
                }

                if (nodes.ContainsKey(tokenName))
                    nodes[tokenName]++;
                else
                    nodes.Add(tokenName, 1);
            }
            else
            {
                if (leafs.ContainsKey(tokenName))
                    leafs[tokenName]++;
                else
                    leafs.Add(tokenName, 1);
            }
        }

        public AnalysisResult Result => new AnalysisResult(nodes, leafs);
    }
}
