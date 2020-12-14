using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Prox_Direction_ItemPicker))]
public class ProxyDirection_Editor : Editor
{
    private void OnSceneGUI()
    {

        Prox_Direction_ItemPicker proxd = (Prox_Direction_ItemPicker)target;

        Handles.color = Color.black;

        Handles.DrawWireDisc(proxd.posInDirection, Vector3.forward, proxd.pickUpRadius);

    }
}
