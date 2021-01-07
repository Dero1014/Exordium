using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipDisplay : MonoBehaviour
{
    public InventoryObject Equipment;

    public List<Transform> EquipSlots = new List<Transform>();

    public Dictionary<GameObject, InventorySlot> ObjToEquipment = new Dictionary<GameObject, InventorySlot>();
    //dictionary to keep the inventory slot to the gameobject
    public Dictionary<InventorySlot, GameObject> EquipDisplayStorage = new Dictionary<InventorySlot, GameObject>();

    [Space(10)]
    public int _degradeAmount;
    public float _degradeSpeed;

    void Start()
    {
        PlayerInput.current.OnInputChange += EquipmentDegrade;
        UpdateDisplay();
    }


    public void UpdateDisplay()
    {
        //does everything as CreateDisplay only on an updating basis
        //the if function checks the Dictionary for what is located in the inventory
        //and if it exists it will just update its value
        //otherwise it creates a new object

        for (int i = 0; i < Equipment.Container.Count; i++)
        {
            if (Equipment.Container[i] != null)
            {
                if (Equipment.Container[i].Item != null)
                {
                    if (EquipDisplayStorage.ContainsKey(Equipment.Container[i]))
                    {
                        EquipDisplayStorage[Equipment.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = Equipment.Container[i].Durrability.ToString();
                    }
                    else
                    {
                        var obj = Instantiate(Equipment.Container[i].Item.Prefab, Vector3.zero, Quaternion.identity, EquipSlots[i]);
                        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = Equipment.Container[i].Durrability.ToString();

                        EquipDisplayStorage.Add(Equipment.Container[i], obj);
                        ObjToEquipment.Add(obj, Equipment.Container[i]);
                    }
                }
          
            }
           
        }
    }

    float _time = 0;
    void EquipmentDegrade()
    {
       
        if (_time < _degradeSpeed)
        {
            _time += Time.deltaTime;
        }
        else
        {
            _time = 0;

            for (int i = 0; i < Equipment.Container.Count; i++)
            {
                if (Equipment.Container[i].Item!=null)
                {
                    Equipment.Container[i].Durrability -= _degradeAmount;
                    if (Equipment.Container[i].Durrability <= 0)
                    {
                        var slot = Equipment.Container[i];
                        var obj = EquipDisplayStorage[slot];

                        EquipDisplayStorage.Remove(slot);
                        Equipment.Container[i].Item = null;
                        Equipment.Container[i].Durrability = 0;
                        ObjToEquipment.Remove(obj);
                        Destroy(obj);
                        print("destroyed");


                    }
                }
            }
            UpdateDisplay();
        }

        

    }

}
