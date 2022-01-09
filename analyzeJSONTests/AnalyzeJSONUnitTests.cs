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

        [Fact]
        public void GetNameFromPath_NullTokenPath()
        {
            Assert.Empty(AnalyzeJSON.GetNameFromPath(null));
        }

        [Fact]
        public void GetNameFromPath_EmptyTokenPath()
        {
            Assert.Empty(AnalyzeJSON.GetNameFromPath(string.Empty));
        }
    }
}
