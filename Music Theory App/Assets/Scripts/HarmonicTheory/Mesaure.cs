using System;
using System.Collections.Generic;

[Serializable]
public class Measure
{
    public TimeSignature timeSignature;
    public List<ChromaticNote> notes;

    public Measure (TimeSignature timeSig)
    {
        timeSignature = timeSig;

        notes = new List<ChromaticNote>();

        /*for (int i = 0; i < timeSignature.Numerator; ++i)
        {
            notes.Add (new Note (Note.NoteName.C, timeSignature.Denominator));
        }*/
    }
}