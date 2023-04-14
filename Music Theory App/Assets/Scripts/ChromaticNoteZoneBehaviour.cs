using System.Collections.Generic;
using UnityEngine;

public class ChromaticNoteZoneBehaviour
{
    
    public HashSet<ChromaticNoteBehaviour> Notes { get; protected set; }

    protected void Awake()
    {
        Notes = new HashSet<ChromaticNoteBehaviour>();
    }

    public void AddNote    (ChromaticNoteBehaviour note) => Notes.Add    (note);
    public void RemoveNote (ChromaticNoteBehaviour note) => Notes.Remove (note);
    
}