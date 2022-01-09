using System;
using analyzeJSON;
using Xunit;

namespace analyzeJSONTests
{
    public class AnalyzeJSONUnitTests
    {
        [Fact]
        public void AnalyzeJSON_NullPath()
        {
            Assert.Throws<ArgumentNullException>(() => new AnalyzeJSON(null));
        }
    }
}
