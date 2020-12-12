using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//check Trigger_Collision_Item to see more info
//Since Trigger_Collision_Item collects and adds items into inventory SO Inventory_Display
//can just check the data that is located inside of the inventorySO and use it to update its
//display on the ui without needing to specificly check what has the player collected
public class Inventory_Display : MonoBehaviour
{
    public Inventory_Object inventory; //since we are using a scriptable object we can just call the SO instead of calling it off another script
   
    public GameObject slots;
    public int startSlots;
    public int maxSlots;
    public int addNumSlots;

    public int xStart; //where the items start 
    public int yStart;
    [Header("This is to put in more rows of the back templates of items but for now no")]
    public int xSpaceBetweenSlots;
    public int numOfColumns; 
    public int ySpaceBetweenSlots;

    [Header("Im gonna have to figure this out later")]
    //these are use to place slots and items into game objects so they don't clutter the inventory panel
    //PS They always need to be in the middle
    public Transform slotsParent;
    public Transform itemsParent;

    public Dictionary<GameObject, Inventory_Slot> objToItems = new Dictionary<GameObject, Inventory_Slot>();
    //dictionary to keep the inventory slot to the gameobject
    public Dictionary<Inventory_Slot, GameObject> itemsDisplayed = new Dictionary<Inventory_Slot, GameObject>();


    private int k = 0;
    void Start()
    {
        CreateDisplay();
        StartSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
        UpdateDisplay();
    }

    public void CreateDisplay() //creates the inventory and if anything is inside it
    {
        //instatiates the items that are located in the inventory
        //and sets them to their proper position and adds them into the
        //dictionary

        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, itemsParent); 
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString();
            itemsDisplayed.Add(inventory.container[i], obj);
            objToItems.Add(obj, inventory.container[i]);
        }
    }

    public void UpdateDisplay()
    {
        //does everything as CreateDisplay only on an updating basis
        //the if function checks the Dictionary for what is located in the inventory
        //and if it exists it will just update its value
        //otherwise it creates a new object

        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                itemsDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString();
                itemsDisplayed[inventory.container[i]].GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
            else
            {
                var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, itemsParent);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.container[i], obj);
                objToItems.Add(obj, inventory.container[i]);
            }
        }
    }

    public void StartSlots()
    {

        for (int i = 0; i < startSlots; i++)
        {
            var obj = Instantiate(slots, Vector3.zero, Quaternion.identity, slotsParent);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        }

    }

    public void UpdateSlots()
    {
        if (inventory.container.Count>maxSlots)
        {
            int upTo = maxSlots + addNumSlots;

            for (int i = maxSlots; i < upTo; i++)
            {
                var obj = Instantiate(slots, Vector3.zero, Quaternion.identity, slotsParent);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }

            maxSlots += addNumSlots; 
        }
    }

    //gets the position needed to be in when the item is placed in inventory
    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xSpaceBetweenSlots * (i % numOfColumns)), yStart + (- ySpaceBetweenSlots * (i / numOfColumns)), 0);
    }

}
