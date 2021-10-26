using Extensions;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Accidental
{
    
    
    
    ////////////////////////////////////////////////////////////////
    // Integer to String Conversion
    
    public static string IntToString (int _value)
    {
        string str = "";

        if (_value > 0) // sharps
        {
            str = new string ('x', _value.GreatestWholeDivisor (2));

            if (_value.IsOdd()) 
            {
                str = str.Insert (0, "#");
            }
        }

        else if (_value < 0) // flats
        {
            str = new string ('b', _value.Abs());
        }

        return str;
    }
    
    
    
    
    ////////////////////////////////////////////////////////////////
    // String to Integer Conversion

    private static Dictionary<char, int> accidentalToIntDict = new Dictionary<char, int> {{'X', 2}, {'x', 2}, {'#', 1}, {'b', -1}};

    public static int StringToInt (string str)
    {
        int brightness = 0;
        
        foreach (char accidental in str)
        {
            if (accidentalToIntDict.ContainsKey (accidental)) 
            {
                brightness += accidentalToIntDict[accidental];
            }
        }
        
        return brightness;
    }
    
    
    
    
    ////////////////////////////////////////////////////////////////
    // Sharp and Flat coordinates
    
    public static Coordinate SharpCoordinate    { get => Coordinate.Sharp; }
    public static Coordinate FlatCoordinate     { get => Coordinate.Flat; }
    public static Coordinate NaturalCoordinate  { get => Coordinate.Natural; }
    
    
    
    
    ////////////////////////////////////////////////////////////////
    // Non-static stuff
    
    
    [SerializeField] private int value = 0;
    
    
    public int IntValue
    {
        get => this.value;
        set => this.value = value;
    }

    public string StringValue
    {
        get => IntToString (this.value);
        set => this.value = StringToInt (value);
    }
    
    
    public Coordinate Cooridnate { get => SharpCoordinate * this.value; }

    
    public Accidental ()           { this.value = 0; }
    public Accidental (int _value) { this.value = _value; }
    public Accidental (string str) { this.value = StringToInt (str); }
}
