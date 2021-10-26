using System;

public static class NumericMethods
{
    private static ulong GreatestCommonDenominator (ulong a, ulong b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b) { a %= b; }
            else       { b %= a; }
        }

        return a | b;
    }
}
