using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipDisplay : MonoBehaviour
{
    public InventoryObject Equipment;

    public List<Transform> EquipSlots = new List<Transform>();

    public Dictionary<GameObject, Inventory_Slot> ObjToEquipment = new Dictionary<GameObject, Inventory_Slot>();
    //dictionary to keep the inventory slot to the gameobject
    public Dictionary<Inventory_Slot, GameObject> EquipDisplayStorage = new Dictionary<Inventory_Slot, GameObject>();


    void Start()
    {
        UpdateDisplay();
    }

    private void Update()
    {
    }

    public void UpdateDisplay()
    {
        //does everything as CreateDisplay only on an updating basis
        //the if function checks the Dictionary for what is located in the inventory
        //and if it exists it will just update its value
        //otherwise it creates a new object
        print("Updated equip");
        for (int i = 0; i < Equipment.Container.Count; i++)
        {
            if (Equipment.Container[i] != null)
            {
                if (Equipment.Container[i].Item != null)
                {
                    if (EquipDisplayStorage.ContainsKey(Equipment.Container[i]))
                    {
                        EquipDisplayStorage[Equipment.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = Equipment.Container[i].Amount.ToString();
                    }
                    else
                    {
                        var obj = Instantiate(Equipment.Container[i].Item.Prefab, Vector3.zero, Quaternion.identity, EquipSlots[i]);
                        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = Equipment.Container[i].Amount.ToString("n0");

                        EquipDisplayStorage.Add(Equipment.Container[i], obj);
                        ObjToEquipment.Add(obj, Equipment.Container[i]);
                    }
                }
          
            }
           
        }
    }

}
