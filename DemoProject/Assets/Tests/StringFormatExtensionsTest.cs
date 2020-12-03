using NUnit.Framework;
using Script.Helpers;

namespace Tests
{
    public class StringFormatExtensionsTest
    {
        
        [TestCase(0, ExpectedResult = "000000000")]
        [TestCase(9, ExpectedResult = "000000009")]
        [TestCase(99, ExpectedResult = "000000099")]
        [TestCase(999, ExpectedResult = "000000999")]
        [TestCase(999999999, ExpectedResult = "999999999")]
        public string FormatScore_Integer(int score)
        {
            return score.FormatScore();
        }
    }
}
