// unset

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DiatonicNote
{
    
    
    
    ////////////////////////////////////////////////////////////////
    // The word of God, but in static methods.
    
    public static NoteName[] DiatonicNames = {NoteName.A, NoteName.B, NoteName.C, NoteName.D, NoteName.E, NoteName.F, NoteName.G};

    public static Dictionary<NoteName, Coordinate> DiatonicCoordinates = new Dictionary<NoteName, Coordinate>
    {
        {NoteName.A, new Coordinate (3,  -1)}, {NoteName.B, new Coordinate (5,  -2)}, 
        {NoteName.C, new Coordinate (0,   0)}, {NoteName.D, new Coordinate (2,  -1)}, 
        {NoteName.E, new Coordinate (4,  -2)}, {NoteName.F, new Coordinate (-1,  1)}, 
        {NoteName.G, new Coordinate (1,   0)},
    };

    public static ModulusWheel<NoteName> DiatonicModWheel { get => new ModulusWheel<NoteName> (DiatonicNames); }
    
    public static NoteName AtWheelPosition (int position)
    {
        return DiatonicModWheel.PeakAtPosition (position);
    }



    ////////////////////////////////////////////////////////////////
    // Shortcuts 

    public static DiatonicNote A { get => new DiatonicNote (NoteName.A); }
    public static DiatonicNote B { get => new DiatonicNote (NoteName.B); }
    public static DiatonicNote C { get => new DiatonicNote (NoteName.C); }
    public static DiatonicNote D { get => new DiatonicNote (NoteName.D); }
    public static DiatonicNote E { get => new DiatonicNote (NoteName.E); }
    public static DiatonicNote F { get => new DiatonicNote (NoteName.F); }
    public static DiatonicNote G { get => new DiatonicNote (NoteName.G); }
    
    
    
    
    ////////////////////////////////////////////////////////////////
    // Non-static stuff
    
    public NoteName   Name       { get; protected set; }
    public Coordinate Coordinate { get => DiatonicCoordinates[Name]; }
    
    public DiatonicNote (NoteName name)
    {
        Name = name;
    }

    public DiatonicNote () : this (NoteName.C) { }
    
    
    public static implicit operator NoteName (DiatonicNote note) => note.Name;
    
    public static implicit operator Coordinate (DiatonicNote note) => DiatonicCoordinates[note];
    
}
