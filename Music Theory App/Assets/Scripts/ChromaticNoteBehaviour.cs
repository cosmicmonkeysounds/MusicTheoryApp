using System;
using System.Collections.Generic;
using UnityEngine;

public class ChromaticNoteBehaviour : MonoBehaviour
{
    
    [field: SerializeField]
    public ChromaticNote Note { get; protected set; }


    public static implicit operator ChromaticNote (ChromaticNoteBehaviour note) => note.Note;

    private void OnTriggerEnter (Collider other)
    {
        if (other.TryGetComponent (out ChromaticNoteBehaviour note))
        {
            
        }
    }


    private void Awake()
    {


}
}