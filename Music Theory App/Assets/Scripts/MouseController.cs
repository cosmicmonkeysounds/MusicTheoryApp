using System;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [field: SerializeField] public GameObject ThingToCreateOnClick { get; protected set; }
    
    public Transform DraggedObject { get; private set; }
    
    public Camera MainCamera { get; protected set; }

    private float cameraZDistance;

    private void Awake()
    {
        MainCamera = GameObject.FindObjectOfType<Camera>();
        cameraZDistance = MainCamera.transform.position.z;
        
        var notes = new List<ChromaticNote>
        {
            new ChromaticNote (DiatonicNote.C, Accidental.Natural, 1),
            new ChromaticNote (DiatonicNote.B),
            new ChromaticNote (DiatonicNote.F)
        };

        Debug.Log (notes.GetBrightness().ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Mouse0))
        {
            
            if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.gameObject.TryGetComponent (out ChromaticNoteBehaviour note))
                {
                    DraggedObject = hit.transform;
                }
            }
        }
        
        else if (Input.GetKeyUp (KeyCode.Mouse0))
        {
            DraggedObject = null;
        }
        
        else if (Input.GetKey (KeyCode.Mouse0))
        {
            if (DraggedObject != null)
            {
                var mousePos = Input.mousePosition;
                mousePos.z = cameraZDistance;
                Vector3 cursorPoint = MainCamera.ScreenToWorldPoint (mousePos);
                cursorPoint.z = 0.0f;
                DraggedObject.position = cursorPoint;
            }
        }
        
        


    }
}