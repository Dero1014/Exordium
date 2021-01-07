using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public static CustomEvents current;

   
    private void Awake()
    {
        current = this;
    }



    #region change in value

    public delegate void ChangeInValues(int newValues);
    public event ChangeInValues OnValueChangeStr;
    public event ChangeInValues OnValueChangeDex;
    public event ChangeInValues OnValueChangeAgi;
    public event ChangeInValues OnValueChangeInt;
    public event ChangeInValues OnValueChangeLck;

    private int _strValue = 0;
    private int _dexValue = 0;
    private int _agiValue = 0;
    private int _intValue = 0;
    private int _lckValue = 0;

    public int StrValue
    {
        get { return _strValue; }
        set
        {
            if (_strValue == value) return;
            _strValue = value;

            if (OnValueChangeStr != null)
            {
                OnValueChangeStr(_strValue);
            }
        }
    }

    public int DexValue
    {
        get { return _dexValue; }
        set
        {
            if (_dexValue == value) return;
            _dexValue = value;

            if (OnValueChangeDex != null)
            {
                OnValueChangeDex(_dexValue);
            }
        }
    }

    public int AgiValue
    {
        get { return _agiValue; }
        set
        {
            if (_agiValue == value) return;
            _agiValue = value;

            if (OnValueChangeAgi != null)
            {
                OnValueChangeAgi(_agiValue);
            }
        }
    }

    public int IntValue
    {
        get { return _intValue; }
        set
        {
            if (_intValue == value) return;
            _intValue = value;

            if (OnValueChangeInt != null)
            {
                OnValueChangeInt(_intValue);
            }
        }
    }

    public int LckValue
    {
        get { return _lckValue; }
        set
        {
            if (_lckValue == value) return;
            _lckValue = value;

            if (OnValueChangeLck != null)
            {
                OnValueChangeLck(_lckValue);
            }
        }
    }

    #endregion

}
