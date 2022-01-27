using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace analyzeJSON
{
    public record Status(bool Success = false, string Message = "");

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

        public AnalyzeJSON(JObject jObject)
        {
            json = jObject ?? throw new ArgumentNullException("jObject");
        }

        public static string GetNameFromPath(string tokenPath)
        {
            if (string.IsNullOrWhiteSpace(tokenPath))
                return string.Empty;

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

        public Status Traverse(Action<JToken> action)
        {
            if (!json.HasValues)
                return new(false, "JSON is empty!");

            if (action == null)
                return new(false, "Action can't be null!");

            Traverse(json.Children(), action);
            return new(true, string.Empty);
        }
    }
}
