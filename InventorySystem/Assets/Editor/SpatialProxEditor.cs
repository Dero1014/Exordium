using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PredefinedSpatialProximity))]
public class SpatialProxEditor : Editor
{

    private void OnSceneGUI()
    {
        PredefinedSpatialProximity psp = (PredefinedSpatialProximity)target;

        Handles.color = Color.red;

        Handles.DrawWireDisc(psp.transform.position, Vector3.forward, psp.MaxDistance);


    }

}
