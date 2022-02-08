using Xunit;
using analyzeJSON;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace analyzeJSONTests
{
    public class ExtractTextUnitTests
    {
        [Fact]
        public void ExtractText_NullKeys()
        {
            Assert.Throws<ArgumentNullException>(() => new ExtractText(null));
        }

        [Fact]
        public void ExtractText_AllGood()
        {
            dynamic token = new JObject();
            token.one = "Lorem ipsum dolor";
            token.two = "Lorem      ipsum dolor";
            token.three = "Lorem\nipsum dolor";

            var keys = new Dictionary<string, bool>
            {
                ["one"] = true,
                ["two"] = false
            };

            var test = new ExtractText(keys);
            test.Extract(token.one);
            test.Extract(token.two);
            test.Extract(token.three);

            Assert.Equal(2, test.Result.Count);
        }
    }
}
