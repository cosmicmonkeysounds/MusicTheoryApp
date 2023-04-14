using System.Collections.Generic;

public class Scale
{ 
    public ModulusWheel<ChromaticNote> notes { get; protected set; }

    public ModulusWheel<ChromaticNote> GetTriad (int degree)
    {
        var triad = new ModulusWheel<ChromaticNote>();
        
        return triad;
    }
}