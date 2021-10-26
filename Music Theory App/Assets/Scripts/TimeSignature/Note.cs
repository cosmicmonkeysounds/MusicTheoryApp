using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Note
{
    [SerializeField] private DiatonicNote diatonicNote;
    [SerializeField] private Accidental accidental;

    // public Note()               : this (Note (NoteName.C, 4)) {} 
    // public Note (NoteName note) : this (Note (note, 4)) {} 
    // public Note (int _length)   : this (Note (NoteName.C, _length)) {}
    
}