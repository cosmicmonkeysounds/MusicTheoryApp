using System.Collections;
using System.Collections.Generic;


public static class CircleOfFifths
{
    public static ModulusWheel<ChromaticNote> Circle { get; } = new ModulusWheel<ChromaticNote> (new ChromaticNote[]
        {
            new ChromaticNote (DiatonicNote.C, Accidental.Natural),
            new ChromaticNote (DiatonicNote.G, Accidental.Natural),
            new ChromaticNote (DiatonicNote.D, Accidental.Natural),
            new ChromaticNote (DiatonicNote.A, Accidental.Natural),
            new ChromaticNote (DiatonicNote.E, Accidental.Natural),
            new ChromaticNote (DiatonicNote.B, Accidental.Natural),
            new ChromaticNote (DiatonicNote.F, Accidental.Sharp),
            new ChromaticNote (DiatonicNote.C, Accidental.Sharp),
            new ChromaticNote (DiatonicNote.A, Accidental.Flat),
            new ChromaticNote (DiatonicNote.E, Accidental.Flat),
            new ChromaticNote (DiatonicNote.B, Accidental.Flat),
            new ChromaticNote (DiatonicNote.F, Accidental.Natural)
        });
}
