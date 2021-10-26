using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChromaticNote
{
    [SerializeField] private DiatonicNote diatonicNote;
    [SerializeField] private Accidental accidental;

    public ChromaticNote (DiatonicNote note, Accidental acci)
    {
        diatonicNote = note;
        accidental   = acci;
    }

    public ChromaticNote()
    {
        diatonicNote = new DiatonicNote ();
    }
}