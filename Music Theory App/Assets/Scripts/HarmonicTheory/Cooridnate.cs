using System;
using UnityEngine;

[Serializable]
public class Cooridnate
{
    [SerializeField] private Vector2Int rawCoordinate;

    public int Semitone { get => (rawCoordinate.x * 7) + (rawCoordinate.y * 12); }

    public Cooridnate (int fifths, int octaves)
    {
        rawCoordinate = new Vector2Int (fifths, octaves);
    }

    public static Cooridnate operator *(Cooridnate coord, int scaler)
    {
        coord.rawCoordinate *= scaler;
        return coord;
    }
    
    public void Scale (int scaler)
    {
        rawCoordinate *= scaler;
    }
}
