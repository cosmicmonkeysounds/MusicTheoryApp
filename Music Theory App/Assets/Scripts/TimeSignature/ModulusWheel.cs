using Extensions;
using System;
using UnityEngine;

[Serializable]
public class ModulusWheel<Type>
{
    [SerializeField] private int numberOfSpokes, currentPosition;

    private Type[] wheel;

    public ModulusWheel (Type[] values)
    {
        wheel           = values;
        numberOfSpokes  = wheel.Length;
        currentPosition = 0;
    }
    

    public void ResetRotation()                { currentPosition = 0; }

    public void Rotate (int numberOfRotations) { currentPosition += numberOfRotations; }

    
    public Type PeakTop()                 { return AtWheelPosition (currentPosition); }
    public Type AtPosition (int position) { return AtWheelPosition (position + currentPosition); }

    private Type AtWheelPosition (int position)
    {
        do { position = (position + numberOfSpokes) % numberOfSpokes; } 
        while (position.IsNegative());

        return wheel[position];
    }
}
