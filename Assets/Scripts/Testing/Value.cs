using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{

    void Start()
    {
        GameEvents.current.onValueChangeTriggerEnter += TheValueHasChanged;
    }

    void TheValueHasChanged(float newValue)
    {
        print("IT HAS CHANGED" + newValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameEvents.current.TheValue += 10;
        }

    }

}
