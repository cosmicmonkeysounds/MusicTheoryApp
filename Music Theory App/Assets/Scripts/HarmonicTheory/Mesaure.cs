using System;
using System.Collections.Generic;

[Serializable]
public class Measure
{
    public TimeSignature timeSignature;
    public List<Note> notes;

    public Measure (TimeSignature timeSig)
    {
        timeSignature = timeSig;

        notes = new List<Note>();

        /*for (int i = 0; i < timeSignature.Numerator; ++i)
        {
            notes.Add (new Note (Note.NoteName.C, timeSignature.Denominator));
        }*/
    }
}