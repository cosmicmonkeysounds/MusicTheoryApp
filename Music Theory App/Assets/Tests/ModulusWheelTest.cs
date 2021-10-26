// unset

using NUnit.Framework;

namespace Tests
{
    public class ModulusWheelTest
    {
        
        
        
        /////////////////////////////////////////////////////////////
        /// Constructor
        
        [Test]
        //[TestCase (new int[]      {1, 2, 3})]
        [TestCase (new NoteName[] {NoteName.A, NoteName.B, NoteName.C})]
        public void ConstructorWorks (NoteName[] values)
        {
            var wheel = new ModulusWheel<NoteName> (values);
            
            for (int i = 0; i < values.Length; ++i)
            {
                Assert.AreEqual (values[i], wheel.PeakAtPosition(i));
            }
        }
        
        
        
        /////////////////////////////////////////////////////////////
        /// PeakTop
        
        [Test]
        [TestCase (new NoteName[] {NoteName.A, NoteName.B, NoteName.C})]
        public void PeakTopWorks (NoteName[] values)
        {
            var wheel = new ModulusWheel<NoteName> (values);
            Assert.AreEqual (values[0], wheel.PeakTop());
        }
        
        
        
        /////////////////////////////////////////////////////////////
        /// Rotation
        
        [Test]
        [TestCase (new NoteName[] {NoteName.A, NoteName.B, NoteName.C})]
        public void RotationWorks (NoteName[] values)
        {
            var wheel = new ModulusWheel<NoteName> (values);

            wheel.Rotate (1);
            Assert.AreEqual (values[1], wheel.PeakTop());

            wheel.Rotate (-1);
            Assert.AreEqual (values[0], wheel.PeakTop());

            wheel.Rotate (values.Length);
            Assert.AreEqual (values[0], wheel.PeakTop());
            
            wheel.Rotate (values.Length * -2);
            Assert.AreEqual (values[0], wheel.PeakTop());
            
            wheel.Rotate (1);
            Assert.AreEqual (values[1], wheel.PeakTop());
        }
        
        
        
        /////////////////////////////////////////////////////////////
        /// Get Position
        
        [Test]
        [TestCase (new NoteName[] {NoteName.A, NoteName.B, NoteName.C, NoteName.D})]
        public void GetPositionWorks (NoteName[] values)
        {
            var wheel = new ModulusWheel<NoteName> (values);

            Assert.AreEqual (values[0], wheel.PeakAtPosition(0));
            Assert.AreEqual (values[0], wheel.PeakAtPosition (values.Length * 2));
            Assert.AreEqual (values[0], wheel.PeakAtPosition (values.Length * -2));
        }
    }
}