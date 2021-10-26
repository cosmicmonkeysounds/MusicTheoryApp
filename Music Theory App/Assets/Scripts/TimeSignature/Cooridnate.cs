using System;
using UnityEngine;

[Serializable]
public class Cooridnate
{
    [SerializeField] private int fifthsOffset, octaveOffset;

    public int Semitone
    {
        get => (fifthsOffset * 7) + (octaveOffset * 12);
    }

    public Cooridnate (int fifths, int octaves)
    {
        fifthsOffset = fifths;
        octaveOffset = octaves;
    }
}
