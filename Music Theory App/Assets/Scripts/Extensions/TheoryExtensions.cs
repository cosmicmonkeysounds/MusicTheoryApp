

using System.Collections.Generic;

public static class TheoryExtensions
{
    public static int GetBrightness (this IEnumerable<ChromaticNote> notes)
    {
        int brightness = 0;
        foreach (var note in notes)
        {
            brightness += note.Coord.Fifths;
        }
        return brightness;
    }
}