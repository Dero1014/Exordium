using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProxyItemPicker))]
public class ProxyEditor : Editor
{
    private void OnSceneGUI()
    {
        ProxyItemPicker proxi = (ProxyItemPicker)target;

        Handles.color = Color.blue;

        Handles.DrawWireDisc(proxi.transform.position, Vector3.forward, proxi.PickUpRadius);

    }
}
