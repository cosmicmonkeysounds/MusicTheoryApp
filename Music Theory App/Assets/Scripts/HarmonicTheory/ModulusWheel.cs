using Extensions;
using System;
using UnityEngine;

[Serializable]
public class ModulusWheel<T>
{
    [SerializeField] private int numberOfSpokes, currentPosition;
    [SerializeField] private bool canBeRotated = true;

    private T[] wheel;

    public ModulusWheel (T[] values, bool _canBeRotated = true)
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

    public T PeakTop()
    {
        return AtWheelPosition (currentPosition);
    }

    public T PeakAtPosition (int position)
    {
        return AtWheelPosition (position + currentPosition);
    }
    
    ////////////////////////////////////////////////////////////////
    // The heart of it

    private T AtWheelPosition (int position)
    {
        do { position = (position + numberOfSpokes) % numberOfSpokes; } 
        while (position.IsNegative());

        return wheel[position];
    }
}
