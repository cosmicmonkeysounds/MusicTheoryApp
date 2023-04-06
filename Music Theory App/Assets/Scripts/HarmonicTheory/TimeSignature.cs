using System;

[Serializable]
public class TimeSignature
{
    
    public float Numerator   { get; protected set; }
    public float Denominator { get; protected set; }


    public TimeSignature (float numerator = 4.0f, float denominator = 4.0f)
    {
        Numerator   = numerator;
        Denominator = denominator;
    }
    
}