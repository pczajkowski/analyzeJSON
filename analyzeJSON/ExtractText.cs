using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public class ExtractText
    {
        private readonly Dictionary<string, bool> keysToExtract;

        public ExtractText(Dictionary<string, bool> keys)
        {
            keysToExtract = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        public void Extract(JToken token)
        {
            if (token.Type != JTokenType.String)
                return;

            var tokenName = AnalyzeJSON.GetNameFromPath(token.Path);
            if (string.IsNullOrWhiteSpace(tokenName))
                return;

            if (!keysToExtract.ContainsKey(tokenName))
                return;

            if (!Result.ContainsKey(tokenName))
                Result.Add(tokenName, new List<string>());

            Result[tokenName].Add(token.Value<string>());
        }

        public Dictionary<string, List<string>> Result { get; } = new();
    }
}
