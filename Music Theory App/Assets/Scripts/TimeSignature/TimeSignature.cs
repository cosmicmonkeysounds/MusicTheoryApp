using System;

[Serializable]
public class TimeSignature
{
    public int numerator, denominator;

    public int Numerator   { get => numerator; }
    public int Denominator { get => denominator; }


    public TimeSignature (int topNumber, int bottomNumber)
    {
        numerator   = topNumber;
        denominator = bottomNumber;
    }
}