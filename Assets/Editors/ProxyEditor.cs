using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Proxy_ItemPicker))]
public class ProxyEditor : Editor
{
    private void OnSceneGUI()
    {
        Proxy_ItemPicker proxi = (Proxy_ItemPicker)target;

        Handles.color = Color.blue;

        Handles.DrawWireDisc(proxi.transform.position, Vector3.forward, proxi.pickUpRadius);

    }
}
