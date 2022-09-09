using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                throw new ArgumentNullException(nameof(path));

            using var sr = new StreamReader(path);
            var jsonString = sr.ReadToEnd();
            json = JsonConvert.DeserializeObject<JObject>(jsonString);
        }

        public AnalyzeJSON(JObject jObject)
        {
            json = jObject ?? throw new ArgumentNullException(nameof(jObject));
        }

        public static string GetNameFromPath(string tokenPath)
        {
            if (string.IsNullOrWhiteSpace(tokenPath))
                return string.Empty;

            var name = tokenPath.Split(".").LastOrDefault();
            return Regex.Replace(name, @"\[\d+?\]$", "");
        }

        private void TraverseWithActions(IJEnumerable<JToken> tokens, List<Action<JToken>> actions)
        {
            foreach (var token in tokens)
            {
                foreach (var action in actions)
                    action.Invoke(token);

                if (token.HasValues)
                    TraverseWithActions(token.Children(), actions);
            }
        }

        public Status Traverse(Action<JToken> action)
        {
            if (!json.HasValues)
                return new(false, "JSON is empty!");

            if (action == null)
                return new(false, $"{nameof(action)} can't be null!");

            TraverseWithActions(json.Children(), new List<Action<JToken>> { action });
            return new(true, string.Empty);
        }

        public Status Traverse(List<Action<JToken>> actions)
        {
            if (!json.HasValues)
                return new(false, "JSON is empty!");

            if (actions == null || actions.Count == 0)
                return new(false, $"{nameof(actions)} can't be null!");

            TraverseWithActions(json.Children(), actions);
            return new(true, string.Empty);
        }
    }
}
