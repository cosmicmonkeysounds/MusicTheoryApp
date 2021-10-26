using NUnit.Framework;
using UnityEngine;

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

        
        
        /////////////////////////////////////////////////////////////
        /// Constructor

        [Test]
        
        [TestCase (0,   0)]
        [TestCase (2,   2)]
        [TestCase (-2, -2)]
        
        public void ConstructorsWorks (int valueIn, int valueExpected)
        {
            Assert.AreEqual (valueExpected, new Accidental(valueIn).IntValue);
        }
        
        
        
        /////////////////////////////////////////////////////////////
        /// Coordinates work

        [Test]

        [TestCase (-2)]
        [TestCase (-1)]
        [TestCase (1)]
        [TestCase (2)]
        
        public void CoordinatesWork (int valueIn)
        {
            var input = new Vector2Int (7, -4);
            var coord = Coordinate.Sharp;

            input *= valueIn;
            coord *= valueIn;
            
            Assert.AreEqual (input.x, coord.Fifths);
            Assert.AreEqual (input.y, coord.Octaves);
        }
    }
}