using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public static PlayerAttributes current;

    private void Awake()
    {
        current = this;
    }
    //public
    public InventoryObject Equipment;
    public InventorySlot Slot;

    public Slider HpSlider;
    public Slider ManaSlider;

    [HideInInspector]public List<ItemBaseObject> PickedUpItems = new List<ItemBaseObject>();

    public int[] Buffs = { 0, 0, 0, 0, 0 };
    public int[] Attributes = {0, 0, 0, 0, 0}; //0- strength, 1- dexterity, 2- agility, 3- inteligence
    
    [Space(10)]
    public int Health;
    public int Mana;

    //private
    private int[] AttributesSaved = { 0, 0, 0, 0, 0, 0, 0 };

    private int _maxMana;
    private int _maxHp;

    public bool buffApplied = false;

    void Start()
    {
        for (int i = 0; i < Attributes.Length; i++)
        {
            AttributesSaved[i] = Attributes[i];
        }

        _maxHp = Health;
        _maxMana = Mana;

    }

    //changing the script to function with OnChangeTrigger
    //this will happen whenever something has been equiped or dequiped as well as change from perm items

    bool _ramp = false;
    bool _overTime = false; 

    public void Update()
    {
        if (_ramp)
        {
            var item = Slot.Item;
            RampValue(item.HoldTime, item.rampItUpTime, item.Buffs[0].Value, item.Buffs[0].Attribute);
            UpdateAttributes();
        }

        if (_overTime)
        {
            var item = Slot.Item;
            OverTime(item.addValueOverTime, item.Buffs[0].Value, item.Buffs[0].Attribute);
            UpdateAttributes();
        }

        HpSlider.value = (float)Health / _maxHp;
        ManaSlider.value = (float)Mana / _maxMana;

        if (Health < 0)
            Health = 0;
        else if (Health > _maxHp)
            Health = _maxHp;

        if (Mana < 0)
            Mana = 0;
        else if (Mana > _maxMana)
            Mana = _maxMana;

    }

    public void UpdateAttributes()
    {
        //apply the stats from equipment to the attributes
        //add them with existing values of that attribute

        int[] atrNums = { 0, 0, 0, 0, 0 };
        

        for (int i = 0; i < Equipment.Container.Count; i++)
        {
            if (Equipment.Container[i].Item)
            {
                if (Equipment.Container[i].Item.Buffs.Length > 0)
                {
                    for (int j = 0; j < Equipment.Container[i].Item.Buffs.Length; j++)
                    {
                        atrNums[(int)Equipment.Container[i].Item.Buffs[j].Attribute] += Equipment.Container[i].Item.Buffs[j].Value;
                    }
                }
            }
        }

        for (int i = 0; i < PickedUpItems.Count; i++)
        {
            for (int j = 0; j < PickedUpItems[i].Buffs.Length; j++)
            {
                atrNums[(int)PickedUpItems[i].Buffs[j].Attribute] += PickedUpItems[i].Buffs[j].Value;
            }
        }

        for (int i = 0; i < Attributes.Length; i++)
        {
            Attributes[i] = atrNums[i];
        }

        CustomEvents.current.StrValue = atrNums[0] + Buffs[0];
        CustomEvents.current.DexValue = atrNums[1] + Buffs[1];
        CustomEvents.current.AgiValue = atrNums[2] + Buffs[2];
        CustomEvents.current.IntValue = atrNums[3] + Buffs[3];
        CustomEvents.current.LckValue = atrNums[4] + Buffs[4];
    }

    public void ApplyBuff()
    {
        var item = Slot.Item;
        if (item.HoldValue)
        {
            StartCoroutine(HoldValue(item.HoldTime, item.Buffs[0].Value, item.Buffs[0].Attribute));
        }
        else if (item.RampValue)
        {
            _ramp = true;
        }
        else if (item.ValueOverTime)
        {
            _overTime = true;
        }

    }

    IEnumerator HoldValue(float timeSet, int buff, AttributesType type)
    {
        buffApplied = true;
        Buffs[(int)type] = buff;

        UpdateAttributes();
        _ramp = false;

        yield return new WaitForSeconds(timeSet);

        Buffs[(int)type] = 0;
        buffApplied = false;

        UpdateAttributes();

    }

    float _timeRamp;
    void RampValue(float holdTime, float timeSet, int buff, AttributesType type)
    {
        if (_timeRamp < timeSet)
        {
            _timeRamp += Time.deltaTime;
            float value = (buff) * (_timeRamp / timeSet); 
            Buffs[(int)type] = (int) value;
        }
        else
        {
            StartCoroutine(HoldValue(holdTime, buff, type));
            _timeRamp = 0;
        }

    }

    float _timeOver = 0;
    float _passedSeconds = 0;
    void OverTime(float timeSet, int buff, AttributesType type)
    {
        if (_timeOver < 1 && _passedSeconds<timeSet)
        {
            _timeOver += Time.deltaTime;
        }
        else if (_timeOver >= 1)
        {
            _timeOver = 0;
            _passedSeconds++;
            Buffs[(int)type] += buff;
        }
        else
        {
            Buffs[(int)type] = 0;
            _overTime = false;
        }
       

    }


}
