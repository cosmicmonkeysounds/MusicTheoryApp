// unset

using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class CoordinateTest
    {
        [Test]
        
        [TestCase (0,  0,  0)]
        [TestCase (2, -1,  2)]
        [TestCase (-4, 2, -2)]

        public void ScalerWorks (int fifths, int octaves, int scaler)
        {
            var input = new Vector2Int (fifths, octaves);
            var coord = new Coordinate (input);

            input *= scaler;
            coord *= scaler;

            Assert.AreEqual (input.x, coord.Fifths);
            Assert.AreEqual (input.y, coord.Octaves);
        }
    }
}