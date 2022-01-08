using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public class AnalyzeStructure
    {
        private Dictionary<string, int> nodes = new Dictionary<string, int>();
        private Dictionary<string, int> leafs = new Dictionary<string, int>();

        public AnalyzeStructure()
        {
        }

        public void AnalyzeToken(JToken token)
        {
            if (token.HasValues)
            {
                if (token.First.Equals(token.Last) &&
                    token.First.Type != JTokenType.Array && token.First.Type != JTokenType.Property && token.First.Type != JTokenType.Object)
                {
                    return;
                }

                if (nodes.ContainsKey(token.Path))
                    nodes[token.Path]++;
                else
                    nodes.Add(token.Path, 1);
            }
            else
            {
                if (leafs.ContainsKey(token.Path))
                    leafs[token.Path]++;
                else
                    leafs.Add(token.Path, 1);
            }
        }
    }
}
