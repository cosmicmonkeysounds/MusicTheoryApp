using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChromaticNote
{
    [field: SerializeField] public DiatonicNote DiatonicNote { get; protected set; }
    [field: SerializeField] public Accidental   Accidental   { get; protected set; }

    public ChromaticNote (DiatonicNote note = DiatonicNote.C, Accidental acci = )
    {
        diatonicNote = note;
        accidental   = acci;
    }

    public ChromaticNote()
    {
        diatonicNote = new DiatonicNote ();
    }
}