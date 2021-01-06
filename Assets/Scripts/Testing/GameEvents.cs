using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private float _myValue = 0;


    private void Awake()
    {
        current = this;
    }

    public delegate void shitHappens(float newVal);
    public event shitHappens onValueChangeTriggerEnter;

    public float TheValue
    {
        get { return _myValue; }
        set
        {
            if (_myValue == value) return;
            _myValue = value;
            if (onValueChangeTriggerEnter != null)
            {
                onValueChangeTriggerEnter(_myValue);
            }
        }
    }

  
}
