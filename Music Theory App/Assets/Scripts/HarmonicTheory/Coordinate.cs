using System;
using UnityEngine;

[Serializable]
public class Coordinate
{
    
    /////////////////////////////////////////////////////////////
    /// The word of god in static properties and methods
    
    
    public static Coordinate Sharp    { get => new Coordinate (7, -4); }
    public static Coordinate Flat     { get => Sharp * -1; }
    public static Coordinate Natural  { get => new Coordinate (0,  0); }
    
    
    
    public static Coordinate operator * (Coordinate coord, int scaler)
    {
        if (scaler != 0)
        {
            coord.rawCoordinate *= scaler;
        }
        
        return coord;
    }
    
    
    
    
    /////////////////////////////////////////////////////////////
    /// Non-static schtuff
    
    
    [SerializeField] private Vector2Int rawCoordinate;

    
    public int Fifths  { get => rawCoordinate.x; }
    public int Octaves { get => rawCoordinate.y; }
    
    
    public int Semitone { get => (rawCoordinate.x * 7) + (rawCoordinate.y * 12); }

    
    public Coordinate (int fifths, int octaves) : this (new Vector2Int (fifths, octaves)) { }
    public Coordinate (Vector2Int raw)
    {
        rawCoordinate = raw;
    }
    

    public void Scale (int scaler) 
    {
        rawCoordinate *= scaler;
    }
}
