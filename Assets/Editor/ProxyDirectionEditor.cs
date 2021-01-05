using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProxDirectionItemPicker))]
public class ProxyDirectionEditor : Editor
{
    private void OnSceneGUI()
    {

        ProxDirectionItemPicker proxd = (ProxDirectionItemPicker)target;

        Handles.color = Color.black;

        Handles.DrawWireDisc(proxd.PosInDirection, Vector3.forward, proxd.PickUpRadius);

    }
}
