using NUnit.Framework;

namespace Tests
{
    public class AccidentalTest
    {
        
        
        
        /////////////////////////////////////////////////////////////
        /// Int To String
        
        [Test]
        
        [TestCase (0, "")]
        
        [TestCase (-1, "b")]
        [TestCase (-2, "bb")]
        [TestCase (-3, "bbb")]
        
        [TestCase (1, "#")]
        [TestCase (2, "x")]
        [TestCase (3, "#x")]
        [TestCase (4, "xx")]
        [TestCase (5, "#xx")]
        
        public void IntToStringIsCorrect (int n, string expected)
        {
            Assert.AreEqual (expected, Accidental.IntToString (n));
        }
        
        
        
        /////////////////////////////////////////////////////////////
        /// String To Int
        
        [Test]
        
        [TestCase ("", 0)]
        
        [TestCase ("b",   -1)]
        [TestCase ("bb",  -2)]
        [TestCase ("bbb", -3)]
        
        [TestCase ("#",   1)]
        [TestCase ("x",   2)]
        [TestCase ("#x",  3)]
        [TestCase ("xx",  4)]
        [TestCase ("#xx", 5)]
        
        [TestCase ("#3xx", 5)]

        public void StringToIntIsCorrect (string str, int expected)
        {
            Assert.AreEqual (expected, Accidental.StringToInt (str));
        }

    }
}