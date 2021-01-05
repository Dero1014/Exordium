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
    public InventoryObject inventory; //since we are using a scriptable object we can just call the SO instead of calling it off another script
   
    public GameObject slots;
    public int startSlots;
    public int maxSlots;
    public int addNumSlots;

    public int xStart; //where the items start 
    public int yStart;
    public int xSpaceBetweenSlots;
    public int numOfColumns; 
    public int ySpaceBetweenSlots;

    //these are use to place slots and items into game objects so they don't clutter the inventory panel
    //PS They always need to be in the middle
    public Transform slotsParent;
    public Transform itemsParent;

    //dictionary to keep the inventory slot to the gameobject
    public Dictionary<GameObject, Inventory_Slot> objToItems = new Dictionary<GameObject, Inventory_Slot>();
    public Dictionary<Inventory_Slot, GameObject> itemsDisplayed = new Dictionary<Inventory_Slot, GameObject>();
    public List<SlotComponent> slotHolders = new List<SlotComponent>();


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

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].Item.Prefab, Vector3.zero, Quaternion.identity, itemsParent); 
            obj.GetComponent<RectTransform>().position = GetFreeSlotPosition();
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].Amount.ToString();
            itemsDisplayed.Add(inventory.Container[i], obj);
            objToItems.Add(obj, inventory.Container[i]);
        }
    }

    public void UpdateDisplay()
    {
        //does everything as CreateDisplay only on an updating basis
        //the if function checks the Dictionary for what is located in the inventory
        //and if it exists it will just update its value
        //otherwise it creates a new object

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].Amount.ToString();
                if (inventory.Container[i].Amount<= 0)
                {
                    GameObject remember = itemsDisplayed[inventory.Container[i]];
                    objToItems.Remove(itemsDisplayed[inventory.Container[i]]);
                    itemsDisplayed.Remove(inventory.Container[i]);
                    Destroy(remember);
                    inventory.Container.Remove(inventory.Container[i]);
                }
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].Item.Prefab, Vector3.zero, Quaternion.identity, itemsParent);
                obj.GetComponent<RectTransform>().position = GetFreeSlotPosition();
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].Amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
                objToItems.Add(obj, inventory.Container[i]);
                
            }
        }
    }

    public void StartSlots()
    {
        for (int i = 0; i < startSlots; i++)
        {

            var obj = Instantiate(slots, Vector3.zero, Quaternion.identity, slotsParent);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            slotHolders.Add(obj.GetComponentInChildren<SlotComponent>());

        }

    }
    
    //gets the position needed to be in when the item is placed in inventory
    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xSpaceBetweenSlots * (i % numOfColumns)), yStart + (-ySpaceBetweenSlots * (i / numOfColumns)), 0);
    }

    public Vector3 GetFreeSlotPosition()
    {

        for (int i = 0; i < slotHolders.Count; i++)
        {
            if (!slotHolders[i]._occupied)
            {
                slotHolders[i]._occupied = true;

                return slotHolders[i].GetComponent<RectTransform>().position;
            }
        }

        return Vector3.zero;

    }

}
