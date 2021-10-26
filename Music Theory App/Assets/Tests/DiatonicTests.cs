// unset

using NUnit.Framework;

namespace Tests
{
    public class DiatonicTests
    {
        [Test]

        [TestCase (-2, NoteName.F)]
        [TestCase (-1, NoteName.G)]
        [TestCase (0,  NoteName.A)]
        [TestCase (1,  NoteName.B)]
        [TestCase (2,  NoteName.C)]
        [TestCase (3,  NoteName.D)]
        [TestCase (4,  NoteName.E)]
        [TestCase (5,  NoteName.F)]
        [TestCase (6,  NoteName.G)]
        [TestCase (7,  NoteName.A)]
        [TestCase (8,  NoteName.B)]
        
        public void DiatonicWheelIsCorrect (int positionToTest, NoteName expected)
        {
            Assert.AreEqual (expected, DiatonicNote.AtWheelPosition (positionToTest));
        }
        
    }
}