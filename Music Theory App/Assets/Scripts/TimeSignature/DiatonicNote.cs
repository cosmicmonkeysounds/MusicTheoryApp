// unset

using System;
using System.Collections.Generic;
using System.Linq;

public class DiatonicNote
{
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
        return DiatonicModWheel.AtPosition (position);
    }
}
