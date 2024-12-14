using NUnit.Framework;
using org.transliteral.panchang;

namespace PanchangTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            HinduPanchang  hinduPanchang = new HinduPanchang();
            hinduPanchang.Compute();

            Assert.Pass();
        }
    }
}