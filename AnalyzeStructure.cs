using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public record AnalysisResult<T>(T Nodes, T Leafs);
    public record Token(string Name, JTokenType Type);

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
            var currentToken = new Token(tokenName, token.Type);

            if (token.HasValues)
            {
                if (token.First.Equals(token.Last) &&
                    token.First.Type != JTokenType.Array &&
                    token.First.Type != JTokenType.Property &&
                    token.First.Type != JTokenType.Object)
                    return;

                if (nodes.ContainsKey(currentToken))
                    nodes[currentToken]++;
                else
                    nodes.Add(currentToken, 1);
            }
            else
            {
                if (leafs.ContainsKey(currentToken))
                    leafs[currentToken]++;
                else
                    leafs.Add(currentToken, 1);
            }
        }

        public AnalysisResult<Dictionary<Token, int>> Result => new(nodes, leafs);
    }
}
