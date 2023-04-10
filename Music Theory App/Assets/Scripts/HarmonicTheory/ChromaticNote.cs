using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChromaticNote
{
    [field: SerializeField] public DiatonicNote DiatonicNote { get; protected set; }
    [field: SerializeField] public Accidental   Accidental   { get; protected set; }

    public ChromaticNote (DiatonicNote note, Accidental acci)
    {
        DiatonicNote = note;
        Accidental   = acci;
    }
    
    public int MidiNote => DiatonicNote.Coordinate.Semitone + Accidental.IntValue;

}