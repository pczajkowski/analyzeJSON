using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public class AnalyzeJSON
    {
        private readonly JObject json;

        public AnalyzeJSON(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentNullException("path");

            using StreamReader sr = new StreamReader(path);
            var jsonString = sr.ReadToEnd();
            json = JsonConvert.DeserializeObject<JObject>(jsonString);
        }

        public static string GetNameFromPath(string tokenPath)
        {
            return tokenPath.Split(".").Last();
        }

        private void Traverse(IJEnumerable<JToken> tokens, Action<JToken> action)
        {
            foreach (var token in tokens)
            {
                action.Invoke(token);
                if (token.HasValues)
                    Traverse(token.Children(), action);
            }
        }

        public void Traverse(Action<JToken> action)
        {
            if (!json.HasValues)
                return;

            Traverse(json.Children(), action);
        }
    }
}
