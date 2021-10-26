// unset

using System;

namespace Extensions
{
    public static class IntExtensions
    {
        public static bool IsNegative (this int value)
        {
            return value < 0;
        }

        public static bool IsOdd (this int value)
        {
            return (value % 2) == 1;
        }

        public static int GreatestWholeDivisor (this int value, int factor) 
        {
            return (int) Math.Floor ((float) value / factor);
        }

        public static int Abs (this int value)
        {
            return value * (value.IsNegative() ? -1 : 1);
        }
    }
}