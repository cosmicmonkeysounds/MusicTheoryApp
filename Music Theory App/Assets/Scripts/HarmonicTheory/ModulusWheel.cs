using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ModulusWheel<T> : IEnumerable<T>
{
    [SerializeField] private int numberOfSpokes, currentPosition;
    [SerializeField] private bool canBeRotated = true;

    private List<T> wheel;

    public ModulusWheel() : this (new T[] {}) { }
    
    

    public ModulusWheel (T[] values, bool _canBeRotated = true)
    {
        wheel           = values.ToList();
        numberOfSpokes  = wheel.Count;
        currentPosition = 0;
        canBeRotated    = _canBeRotated;
    }

    public ModulusWheel<T> Slice (int start, int end)
    {
        var sliceWheel = new ModulusWheel<T>();

        return sliceWheel;
    }




    ////////////////////////////////////////////////////////////////
    // Rotations

    public void ResetRotation()
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

    
    public IEnumerator<T> GetEnumerator()
    {
        
    }
    

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
