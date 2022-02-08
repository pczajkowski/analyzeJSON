using Xunit;
using analyzeJSON;
using System;

namespace analyzeJSONTests
{
    public class ExtractTextUnitTests
    {
        [Fact]
        public void ExtractText_NullKeys()
        {
            Assert.Throws<ArgumentNullException>(() => new ExtractText(null));
        }
    }
}
