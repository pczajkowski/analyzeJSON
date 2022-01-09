using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public record AnalysisResult<T>(T Nodes, T Leafs);
    public record Token(string Name, JTokenType Type, bool IsLeaf = false);

    public class AnalyzeStructure
    {
        private Dictionary<Token, int> nodes = new Dictionary<Token, int>();
        private Dictionary<Token, int> leafs = new Dictionary<Token, int>();

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
                var nodeToken = new Token(tokenName, token.Type);

                if (token.First.Equals(token.Last) &&
                    token.First.Type != JTokenType.Array &&
                    token.First.Type != JTokenType.Property &&
                    token.First.Type != JTokenType.Object)
                    return;

                if (nodes.ContainsKey(nodeToken))
                    nodes[nodeToken]++;
                else
                    nodes.Add(nodeToken, 1);
            }
            else
            {
                var leafToken = new Token(tokenName, token.Type, true);

                if (leafs.ContainsKey(leafToken))
                    leafs[leafToken]++;
                else
                {
                    leafs.Add(leafToken, 1);
                }
            }
        }

        public AnalysisResult<Dictionary<Token, int>> Result => new(nodes, leafs);
    }
}
