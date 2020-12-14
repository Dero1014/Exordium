using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Predefined_Spatial_Proximity))]
public class SpatialProxEditor : Editor
{

    private void OnSceneGUI()
    {
        Predefined_Spatial_Proximity psp = (Predefined_Spatial_Proximity)target;

        Handles.color = Color.red;

        Handles.DrawWireDisc(psp.transform.position, Vector3.forward, psp.maxDistance);


    }

}
