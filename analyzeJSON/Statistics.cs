using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace analyzeJSON
{
    public record WordCountResult<T>(T NodeCounts, int TotalWordCount);
    public class Statistics
    {
        private readonly Dictionary<string, int> nodeCounts = new Dictionary<string, int>();
        private int totalWordCount;

        private static int CountWords(string text)
        {
            var words = text.Split(" ");
            return words.Count(x => !string.IsNullOrWhiteSpace(x));
        }

        public void RunStatistics(JToken token)
        {
            if (token.Type != JTokenType.String)
                return;

            var tokenName = AnalyzeJSON.GetNameFromPath(token.Path);
            if (!nodeCounts.ContainsKey(tokenName))
                nodeCounts.Add(tokenName, 0);

            var wordCount = CountWords(token.Value<string>());
            nodeCounts[tokenName] += wordCount;
            totalWordCount += wordCount;
        }

        public WordCountResult<Dictionary<string, int>> Result => new(nodeCounts, totalWordCount);
    }
}
