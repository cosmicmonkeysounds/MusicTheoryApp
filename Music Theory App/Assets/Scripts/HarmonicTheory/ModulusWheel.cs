using Extensions;
using System;
using UnityEngine;

[Serializable]
public class ModulusWheel<Type>
{
    [SerializeField] private int numberOfSpokes, currentPosition;
    [SerializeField] private bool canBeRotated = true;

    private Type[] wheel;

    public ModulusWheel (Type[] values, bool _canBeRotated = true)
    {
        wheel           = values;
        numberOfSpokes  = wheel.Length;
        currentPosition = 0;
        canBeRotated    = _canBeRotated;
    }

    
    
    
    ////////////////////////////////////////////////////////////////
    // Rotations

    public void ResetRotation ()
    {
        currentPosition = 0;
    }

    public void Rotate (int numberOfRotations)
    {
        if (canBeRotated)
        {
            currentPosition += numberOfRotations;
        }
    }
    
    
    
    
    ////////////////////////////////////////////////////////////////
    // Fetch from positions

    public Type PeakTop()
    {
        return AtWheelPosition (currentPosition);
    }

    public Type PeakAtPosition (int position)
    {
        return AtWheelPosition (position + currentPosition);
    }
    
    ////////////////////////////////////////////////////////////////
    // The heart of it

    private Type AtWheelPosition (int position)
    {
        do { position = (position + numberOfSpokes) % numberOfSpokes; } 
        while (position.IsNegative());

        return wheel[position];
    }
}
