using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public record AnalysisResult<T>(T Nodes, T Leafs);

    public record Token(string Name, JTokenType Type, bool IsNode = false)
    {
        public int Count;
    }

    public class AnalyzeStructure
    {
        private Dictionary<string, Token> nodes = new Dictionary<string, Token>();
        private Dictionary<string, Token> leafs = new Dictionary<string, Token>();

        public void AnalyzeToken(JToken token)
        {
            if (token.Type == JTokenType.Object || token.Type == JTokenType.Array)
                return;

            var tokenName = AnalyzeJSON.GetNameFromPath(token.Path);

            if (token.HasValues)
            {
                var nodeToken = new Token(tokenName, token.Type, true);

                if (token.First.Equals(token.Last) &&
                    token.First.Type != JTokenType.Array &&
                    token.First.Type != JTokenType.Property &&
                    token.First.Type != JTokenType.Object)
                    return;

                if (nodes.ContainsKey(tokenName))
                    nodes[tokenName].Count++;
                else
                {
                    nodeToken.Count++;
                    nodes.Add(tokenName, nodeToken);
                }
            }
            else
            {
                var leafToken = new Token(tokenName, token.Type);

                if (leafs.ContainsKey(tokenName))
                    leafs[tokenName].Count++;
                else
                {
                    leafToken.Count++;
                    leafs.Add(tokenName, leafToken);
                }
            }
        }

        public AnalysisResult<Dictionary<string, Token>> Result
        {
            get
            {
                foreach (var leaf in leafs)
                {
                    if (nodes.ContainsKey(leaf.Key))
                        leafs[leaf.Key] = leaf.Value with { IsNode = true };
                }

                return new(nodes, leafs);
            }
        }
    }
}
