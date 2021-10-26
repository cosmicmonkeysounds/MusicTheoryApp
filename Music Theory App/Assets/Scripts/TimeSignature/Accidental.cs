using Extensions;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Accidental
{
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

    public Accidental (int _value) { this.value = _value; }
    public Accidental (string str) { this.value = StringToInt (str); }
    
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
}
