using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Equip_Display : MonoBehaviour
{
    public Inventory_Object equipment;

    public List<Transform> equipSlots = new List<Transform>();

    public Dictionary<GameObject, Inventory_Slot> objToEquipment = new Dictionary<GameObject, Inventory_Slot>();
    //dictionary to keep the inventory slot to the gameobject
    public Dictionary<Inventory_Slot, GameObject> equipDisplay = new Dictionary<Inventory_Slot, GameObject>();
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        //does everything as CreateDisplay only on an updating basis
        //the if function checks the Dictionary for what is located in the inventory
        //and if it exists it will just update its value
        //otherwise it creates a new object

        for (int i = 0; i < equipment.container.Count; i++)
        {
            if (equipment.container[i] != null)
            {
                if (equipment.container[i].item != null)
                {
                    if (equipDisplay.ContainsKey(equipment.container[i]))
                    {
                        equipDisplay[equipment.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = equipment.container[i].amount.ToString();
                        equipDisplay[equipment.container[i]].GetComponent<RectTransform>().localPosition = Vector3.zero;
                    }
                    else
                    {
                        //print("This " + equipment.container[i].item.name);

                        //print("New");
                        var obj = Instantiate(equipment.container[i].item.prefab, Vector3.zero, Quaternion.identity, equipSlots[i]);
                        obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
                        obj.GetComponentInChildren<TextMeshProUGUI>().text = equipment.container[i].amount.ToString("n0");

                        equipDisplay.Add(equipment.container[i], obj);
                        objToEquipment.Add(obj, equipment.container[i]);
                    }
                }
          
            }
           
        }
    }

}
