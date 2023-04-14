using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChromaticNote
{
    [field: SerializeField] public DiatonicNote DiatonicNote { get; protected set; }
    [field: SerializeField] public Accidental   Accidental   { get; protected set; }
    
    [field: SerializeField] public int          Octave       { get; protected set; }

    
    public ChromaticNote (DiatonicNote note, int oct = 0) : this (note, Accidental.Flat, oct) { }

    public ChromaticNote (DiatonicNote note, Accidental acci, int oct = 0)
    {
        DiatonicNote = note;
        Accidental   = acci;
        Octave       = oct;
    }
    
    public int MidiNote => DiatonicNote.Coordinate.Semitone + Accidental.IntValue + 13 + (Octave * 12);

    public Coordinate Coord => DiatonicNote.Coordinate + Accidental.Cooridnate;

}