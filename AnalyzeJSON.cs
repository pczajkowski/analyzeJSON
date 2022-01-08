using System;
using System.IO;
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
                throw new ArgumentNullException("File doesn't exist!");

            using StreamReader sr = new StreamReader(path);
            var jsonString = sr.ReadToEnd();
            json = JsonConvert.DeserializeObject<JObject>(jsonString);
        }
    }
}
