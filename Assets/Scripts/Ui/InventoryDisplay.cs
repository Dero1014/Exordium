using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//check Trigger_Collision_Item to see more info
//Since Trigger_Collision_Item collects and adds items into inventory SO Inventory_Display
//can just check the data that is located inside of the inventorySO and use it to update its
//display on the ui without needing to specificly check what has the player collected
public class InventoryDisplay : MonoBehaviour
{
    public InventoryObject Inventory; //since we are using a scriptable object we can just call the SO instead of calling it off another script
   
    public GameObject Slots;
    public int StartNumSlots;
    public int MaxSlots;
    public int AddNumSlots;

    public int XStart; //where the items start 
    public int YStart;
    public int XSpaceBetweenSlots;
    public int NumOfColumns; 
    public int YSpaceBetweenSlots;

    //these are use to place slots and items into game objects so they don't clutter the inventory panel
    //PS They always need to be in the middle
    public Transform SlotsParent;
    public Transform ItemsParent;

    //dictionary to keep the inventory slot to the gameobject
    public Dictionary<GameObject, Inventory_Slot> ObjToItems = new Dictionary<GameObject, Inventory_Slot>();
    public Dictionary<Inventory_Slot, GameObject> ItemsDisplayed = new Dictionary<Inventory_Slot, GameObject>();
    public List<SlotComponent> SlotHolders = new List<SlotComponent>();


    void Awake()
    {
        CreateDisplay();
        StartSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay() //creates the inventory and if anything is inside it
    {
        //instatiates the items that are located in the inventory
        //and sets them to their proper position and adds them into the
        //dictionary

        for (int i = 0; i < Inventory.Container.Count; i++)
        {
            var obj = Instantiate(Inventory.Container[i].Item.Prefab, Vector3.zero, Quaternion.identity, ItemsParent); 
            obj.GetComponent<RectTransform>().position = GetFreeSlotPosition();
            obj.GetComponentInChildren<TextMeshProUGUI>().text = Inventory.Container[i].Amount.ToString();
            ItemsDisplayed.Add(Inventory.Container[i], obj);
            ObjToItems.Add(obj, Inventory.Container[i]);
        }
    }

    public void UpdateDisplay()
    {
        //does everything as CreateDisplay only on an updating basis
        //the if function checks the Dictionary for what is located in the inventory
        //and if it exists it will just update its value
        //otherwise it creates a new object

        for (int i = 0; i < Inventory.Container.Count; i++)
        {
            if (ItemsDisplayed.ContainsKey(Inventory.Container[i]))
            {
                ItemsDisplayed[Inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = Inventory.Container[i].Amount.ToString();
                if (Inventory.Container[i].Amount<= 0)
                {
                    GameObject remember = ItemsDisplayed[Inventory.Container[i]];
                    ObjToItems.Remove(ItemsDisplayed[Inventory.Container[i]]);
                    ItemsDisplayed.Remove(Inventory.Container[i]);
                    Destroy(remember);
                    Inventory.Container.Remove(Inventory.Container[i]);
                }
            }
            else
            {
                var obj = Instantiate(Inventory.Container[i].Item.Prefab, Vector3.zero, Quaternion.identity, ItemsParent);
                obj.GetComponent<RectTransform>().position = GetFreeSlotPosition();
                obj.GetComponentInChildren<TextMeshProUGUI>().text = Inventory.Container[i].Amount.ToString("n0");
                ItemsDisplayed.Add(Inventory.Container[i], obj);
                ObjToItems.Add(obj, Inventory.Container[i]);
                print(ObjToItems.Count);
                print("Added");
            }
        }
    }

    public void StartSlots()
    {
        for (int i = 0; i < StartNumSlots; i++)
        {

            var obj = Instantiate(Slots, Vector3.zero, Quaternion.identity, SlotsParent);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            SlotHolders.Add(obj.GetComponentInChildren<SlotComponent>());

        }

    }
    
    //gets the position needed to be in when the item is placed in inventory
    public Vector3 GetPosition(int i)
    {
        return new Vector3(XStart + (XSpaceBetweenSlots * (i % NumOfColumns)), YStart + (-YSpaceBetweenSlots * (i / NumOfColumns)), 0);
    }

    public Vector3 GetFreeSlotPosition()
    {

        for (int i = 0; i < SlotHolders.Count; i++)
        {
            if (!SlotHolders[i]._occupied)
            {
                SlotHolders[i]._occupied = true;

                return SlotHolders[i].GetComponent<RectTransform>().position;
            }
        }

        return Vector3.zero;

    }

}
