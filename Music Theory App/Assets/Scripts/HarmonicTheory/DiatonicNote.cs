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

    public static Dictionary<NoteName, Cooridnate> DiatonicCoordinates = new Dictionary<NoteName, Cooridnate>
    {
        {NoteName.A, new Cooridnate (3,  -1)}, {NoteName.B, new Cooridnate (5,  -2)}, 
        {NoteName.C, new Cooridnate (0,   0)}, {NoteName.D, new Cooridnate (2,  -1)}, 
        {NoteName.E, new Cooridnate (4,  -2)}, {NoteName.F, new Cooridnate (-1,  1)}, 
        {NoteName.G, new Cooridnate (1,   0)},
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
    
    public NoteName   Name       { get; private set; }
    public Cooridnate Coordinate { get => DiatonicCoordinates[Name]; }
    
    public DiatonicNote (NoteName name)
    {
        Name = name;
    }

    public DiatonicNote () : this (NoteName.C) { }
}
