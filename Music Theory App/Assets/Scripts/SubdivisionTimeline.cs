using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SubdivisionTimeline : MonoBehaviour
{
    [SerializeField] private Measure measure;

    [SerializeField] private GameObject timelineBase;

    [SerializeField] private GameObject downbeatTickPrefab;

    private GameObject tick;

    private Camera camera;

    private void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        tick = PrefabUtility.InstantiatePrefab (downbeatTickPrefab) as GameObject;

        tick.transform.parent = this.transform;

        tick.transform.localPosition = new Vector3 (-(timelineBase.transform.localScale.x / 2), 0, 0);
    }
}
