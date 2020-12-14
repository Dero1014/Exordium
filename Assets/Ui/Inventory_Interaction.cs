using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Interaction : MonoBehaviour
{
    //PUBLIC
    public Inventory_Display iDisplay;
    public Equip_Display eDisplay;
    [Space(10)]
    public Tooltip_Window toolTip;
    public GameObject prefab;
    [Space(10)]

    public GameObject[] uis;

    //PRIVATE
    private bool dragging = false;
    private bool clicked = false; //clicked is used to have the ability to click and pickup or place an item in inventory

    private Player_Attributes pAttributes; //make a SO for attributes;

    private Vector2 originalPosition;
    private Transform objectToDrag;
    private Image objectToDragImage;

    private Transform target;
    private Slot_Component slotOfTheObjHolder;


    List<RaycastResult> hitObjects = new List<RaycastResult>(); //saves all of the raycast results under the mouse

    private void Start()
    {
        pAttributes = GameObject.FindObjectOfType<Player_Attributes>();
    }

    bool itsOnInventory = false;
    bool itsOnEquipment = false;
    bool wasOnEquipment = false;

    void Update()
    {

        if (!uis[0].activeSelf && !uis[1].activeSelf)
        {
            if (objectToDrag != null)
            {
                
                objectToDrag.position = originalPosition;
                slotOfTheObjHolder.occupied = true;

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
            clicked = false;
        }

        #region hover
        if (GetDraggableTransformUnderMouse())
        {
            target = GetDraggableTransformUnderMouse();
            if (target!=null)
            {
                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Inventory_Slot newSlot = iDisplay.objToItems[target.gameObject];

                    GameObject clone = Instantiate(prefab, GameObject.FindObjectOfType<Player_Input>().gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                    clone.GetComponent<Item_Component>().itemType = iDisplay.objToItems[target.gameObject].item;
                    clone.GetComponent<Item_Component>().amount = iDisplay.objToItems[target.gameObject].amount;

                    GameObject remember = target.gameObject;
                    iDisplay.objToItems.Remove(target.gameObject);
                    iDisplay.itemsDisplayed.Remove(newSlot);
                    Destroy(remember);
                    iDisplay.inventory.container.Remove(newSlot);

                    slotOfTheObjHolder.occupied = false;
                }


                toolTip.panel.SetActive(true);
                Inventory_Slot slot;


                if (iDisplay.objToItems.ContainsKey(target.gameObject))
                {
                    slot = iDisplay.objToItems[target.gameObject];
                    toolTip.slot = slot;
                }
                else if (eDisplay.objToEquipment.ContainsKey(target.gameObject))
                {
                    slot = eDisplay.objToEquipment[target.gameObject];
                    toolTip.slot = slot;
                }
                else
                {
                    toolTip.slot = null;
                }
            }
        }
        else
        {
            toolTip.slot = null;
            toolTip.panel.SetActive(false);
        }

        #endregion

        #region RightClick equip

        if (Input.GetMouseButtonDown(1) && !dragging)
        {
            //later try doing this with iDs

            target = GetDraggableTransformUnderMouse();
            Inventory_Slot slot;

            bool equiped = false; 

            if (target!=null)
            {
                if (iDisplay.objToItems.ContainsKey(target.gameObject))
                {
                    slot = iDisplay.objToItems[target.gameObject];
                    
                    //Move the item from inventory to equip
                    for (int i = 0; i < eDisplay.equipment.container.Count; i++)
                    {
                        if (slot.item.equipType == eDisplay.equipment.container[i].allowedEquip[0])
                        {
                            if (eDisplay.equipment.container[i].item == null)
                            {
                                eDisplay.equipment.container[i].item = slot.item;
                                eDisplay.equipment.container[i].amount = slot.amount;
                                equiped = true;
                                
                                break;
                            }
                        }
                    }

                    if (!equiped)
                    {
                        for (int i = 0; i < eDisplay.equipment.container.Count; i++)
                        {
                            if (slot.item.equipType == eDisplay.equipment.container[i].allowedEquip[0])
                            {
                                if (eDisplay.equipment.container[i].item != null)
                                {

                                    //remember the game object 
                                    GameObject remember = eDisplay.equipDisplay[eDisplay.equipment.container[i]];

                                    eDisplay.equipDisplay.Remove(eDisplay.objToEquipment[eDisplay.equipDisplay[eDisplay.equipment.container[i]]]);

                                    iDisplay.inventory.AddItem(eDisplay.equipment.container[i].item, eDisplay.equipment.container[i].amount);
                                    Destroy(remember);

                                    eDisplay.equipment.container[i].item = slot.item;
                                    eDisplay.equipment.container[i].amount = slot.amount;
                                    break;
                                }

                            }
                        }
                    }

                    //now remove that item from the inventory
                    if (slot.item.type == ItemType.Equipable)
                    {
                        slotOfTheObjHolder.occupied = false;
                        iDisplay.objToItems.Remove(target.gameObject);
                        iDisplay.itemsDisplayed.Remove(slot);
                        iDisplay.inventory.container.Remove(slot);
                        Destroy(target.gameObject);
                    }

                }
                else if (eDisplay.objToEquipment.ContainsKey(target.gameObject))
                {
                    slot = eDisplay.objToEquipment[target.gameObject];
                    iDisplay.inventory.AddItem(slot.item, slot.amount);
                    print("Removed");

                    //remove the fucker
                    eDisplay.objToEquipment.Remove(target.gameObject);
                    eDisplay.equipDisplay.Remove(slot);
                    eDisplay.equipment.container[eDisplay.equipment.container.IndexOf(slot)].item = null;
                    eDisplay.equipment.container[eDisplay.equipment.container.IndexOf(slot)].amount = 0;
                    Destroy(target.gameObject);

                }

            }

        }

        #endregion

        #region ConsumeItem

        if (Input.GetMouseButtonDown(2))
        {
            target = GetDraggableTransformUnderMouse();
            if (target!=null)
            {
                Inventory_Slot slot;
                slot = iDisplay.objToItems[target.gameObject];

                if (slot.item.type == ItemType.Default && slot.item.buffs.Length > 0)
                {
                    slot.amount--;
                    if (slot.amount <= 0)
                    {
                        slotOfTheObjHolder.occupied = false;
                    }
                }
            }
            

        }

        #endregion

        #region pick up and dragg
        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
            clicked = true;
        }

        if (Input.GetMouseButtonDown(0) && !dragging)
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
            {
                wasOnEquipment = OnEquipDisplay();
                itsOnInventory = OnInvDisplay();
                itsOnEquipment = OnEquipDisplay();

                dragging = true;

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponentInChildren<Image>();
                objectToDragImage.raycastTarget = false;
                slotOfTheObjHolder.occupied = false;
            }
        }

        

        if (Input.GetMouseButtonDown(0) && clicked)
        {
            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();
                //Found an object
                if (objectToReplace != null)
                {
                    OrganizeItem(objectToReplace);
                }
                else
                {
                    if (eDisplay.objToEquipment.ContainsKey(objectToDrag.gameObject))
                    {
                        Inventory_Slot slot = eDisplay.objToEquipment[objectToDrag.gameObject];

                        GameObject clone = Instantiate(prefab, GameObject.FindObjectOfType<Player_Input>().gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                        clone.GetComponent<Item_Component>().itemType = eDisplay.objToEquipment[objectToDrag.gameObject].item;
                        clone.GetComponent<Item_Component>().amount = eDisplay.objToEquipment[objectToDrag.gameObject].amount;

                        GameObject remember = objectToDrag.gameObject;
                        eDisplay.objToEquipment.Remove(objectToDrag.gameObject);
                        eDisplay.equipDisplay.Remove(slot);
                        Destroy(remember);
                        eDisplay.equipment.container.Remove(slot);

                        slotOfTheObjHolder.occupied = false;
                    }
                    else
                    {
                        Inventory_Slot slot = iDisplay.objToItems[objectToDrag.gameObject];

                        GameObject clone = Instantiate(prefab, GameObject.FindObjectOfType<Player_Input>().gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                        clone.GetComponent<Item_Component>().itemType = iDisplay.objToItems[objectToDrag.gameObject].item;
                        clone.GetComponent<Item_Component>().amount = iDisplay.objToItems[objectToDrag.gameObject].amount;

                        GameObject remember = objectToDrag.gameObject;
                        iDisplay.objToItems.Remove(objectToDrag.gameObject);
                        iDisplay.itemsDisplayed.Remove(slot);
                        Destroy(remember);
                        iDisplay.inventory.container.Remove(slot);

                        slotOfTheObjHolder.occupied = false;
                    }

                   
                }
               
                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
            clicked = false;
        }

    

        #endregion

    }

    void OrganizeItem(Transform objToReplace)
    {
        //check where the picked up item came from
        if (iDisplay.objToItems.ContainsKey(objectToDrag.gameObject))
        {
            //check where we are leaving it
            itsOnInventory = OnInvDisplay();
            itsOnEquipment = OnEquipDisplay();

            if (itsOnInventory)
            {
                //regulary do your thing
                if (objToReplace.GetComponent<Slot_Component>()) //first check if its over an empty slot
                {
                    objectToDrag.position = objToReplace.position;
                    slotOfTheObjHolder.occupied = false; //set the slot we took it from to unoccupied
                    objToReplace.GetComponent<Slot_Component>().occupied = true;
                }
                else
                {
                    objectToDrag.position = objToReplace.position;
                    objToReplace.position = originalPosition;
                    slotOfTheObjHolder.occupied = true; //because you are just switching out values in the slot they both stay occupied
                }

            }
            else if (itsOnEquipment)
            {
                //so its on equipment now, time to mess about and see what we hit
                if (objToReplace.GetComponent<Slot_Component>()) //first check if its over an empty slot
                {
                    //check which type the slot is
                    int numOfSlot = eDisplay.equipSlots.IndexOf(objToReplace.GetComponentInParent<Slot_Component>().transform); //first get the number of that slot

                    //then compare it with the equip type
                    Inventory_Slot slot = iDisplay.objToItems[objectToDrag.gameObject]; 

                    DragItemInEquipSlot(slot, numOfSlot);
                    
                }
                else
                {
                    //check which type the slot is
                    int numOfSlot = eDisplay.equipSlots.IndexOf(objToReplace.GetComponentInParent<Slot_Component>().transform); //first get the number of that slot
                    print(numOfSlot);

                    //then compare it with the equip type
                    Inventory_Slot slot = iDisplay.objToItems[objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);
                }
            }

        }

        if (eDisplay.objToEquipment.ContainsKey(objectToDrag.gameObject))
        {
            print("It belongs to equipment");

            //check where we are leaving it
            itsOnInventory = OnInvDisplay();
            itsOnEquipment = OnEquipDisplay();

            if (itsOnInventory)
            {
                int numOfSlot = eDisplay.equipSlots.IndexOf(objectToDrag.GetComponentInParent<Slot_Component>().transform); //first get the number of that slot
                Inventory_Slot slot = eDisplay.objToEquipment[objectToDrag.gameObject];
                iDisplay.inventory.AddItem(eDisplay.equipment.container[numOfSlot].item, eDisplay.equipment.container[numOfSlot].amount);
                eDisplay.equipment.container[numOfSlot].item = null;

                eDisplay.objToEquipment.Remove(objectToDrag.gameObject);
                eDisplay.equipDisplay.Remove(slot);
                Destroy(objectToDrag.gameObject);

                objectToDrag.position = objToReplace.position;
                slotOfTheObjHolder.occupied = false; //set the slot we took it from to unoccupied

            }
            else if (itsOnEquipment)
            {
                //so its on equipment now, time to mess about and see what we hit
                if (objToReplace.GetComponent<Slot_Component>()) //first check if its over an empty slot
                {
                    //check which type the slot is
                    int numOfSlot = eDisplay.equipSlots.IndexOf(objToReplace.GetComponentInParent<Slot_Component>().transform); //first get the number of that slot

                    //then compare it with the equip type
                    Inventory_Slot slot = eDisplay.objToEquipment[objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);

                }
                else
                {
                    //check which type the slot is
                    int numOfSlot = eDisplay.equipSlots.IndexOf(objToReplace.GetComponentInParent<Slot_Component>().transform); //first get the number of that slot
                    print(numOfSlot);

                    //then compare it with the equip type
                    Inventory_Slot slot = eDisplay.objToEquipment[objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);
                }
            }
        }

    }

    void DragItemInEquipSlot(Inventory_Slot slot, int numOfSlot) //here is where we check what to do with the lot we are placing on
    {
        bool equiped = false;

        if (slot.item.equipType == eDisplay.equipment.container[numOfSlot].allowedEquip[0])
        {
            if (eDisplay.equipment.container[numOfSlot].item == null) //if its empty
            {
                eDisplay.equipment.container[numOfSlot].item = slot.item; //fill the equipment slot
                eDisplay.equipment.container[numOfSlot].amount = slot.amount;
                equiped = true;
            }
            else
            {
                //remember the game object we are replacing
                GameObject remember = eDisplay.equipDisplay[eDisplay.equipment.container[numOfSlot]];

                eDisplay.equipDisplay.Remove(eDisplay.objToEquipment[eDisplay.equipDisplay[eDisplay.equipment.container[numOfSlot]]]); //remove the current from the display

                iDisplay.inventory.AddItem(eDisplay.equipment.container[numOfSlot].item, eDisplay.equipment.container[numOfSlot].amount); //and the one we replaced back to the inventory
                Destroy(remember); //destroy the object we are replacing

                eDisplay.equipment.container[numOfSlot].item = slot.item; //fill the equipment slot 
                eDisplay.equipment.container[numOfSlot].amount = slot.amount;
                equiped = true;
            }

        }
        else
        {
            //if the type doesn't match return the object
            objectToDrag.position = originalPosition;
            slotOfTheObjHolder.occupied = true;
        }

        //if it fits remove it from inventory
        if (equiped)
        {
            iDisplay.objToItems.Remove(objectToDrag.gameObject);
            iDisplay.itemsDisplayed.Remove(slot);
            iDisplay.inventory.container.Remove(slot);
            Destroy(objectToDrag.gameObject);
        }

    }

    //IEnumerator ApplyConsumable(Inventory_Slot slot, int at)
    //{
    //    pAttributes.attributes[(int)slot.item.buffs[at].attribute] += slot.item.buffs[at].value;
    //    print("start");
    //    yield return new WaitForSeconds(5);
    //    print("no");
    //    pAttributes.attributes[(int)slot.item.buffs[at].attribute] -= slot.item.buffs[at].value;
    //}

    bool OnInvDisplay()
    {

        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return false;

        foreach (var hit in hitObjects)
        {
            if (hit.gameObject.GetComponent<Inventory_Display>())
            {
                return true;
            }
        }

        return false;
    }

    bool OnEquipDisplay()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return false;

        foreach (var hit in hitObjects)
        {
            if (hit.gameObject.GetComponent<Equip_Display>())
            {
                return true;
            }
        }

        return false;
    }


    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        if (dragging)
        {
            foreach (var hit in hitObjects)
            {
                if (hit.gameObject.GetComponent<Item_Ui_Component>())
                {
                    return hit.gameObject;
                }
                if (hit.gameObject.GetComponent<Slot_Component>())
                {
                    return hit.gameObject;
                }
            }
        }
        else
        {
            foreach (var hit in hitObjects)
            {
                if (hit.gameObject.GetComponent<Item_Ui_Component>())
                {
                    foreach (var slotHold in hitObjects)
                    {
                        if (slotHold.gameObject.GetComponent<Slot_Component>())
                        {
                            slotOfTheObjHolder = slotHold.gameObject.GetComponent<Slot_Component>();
                        }
                    }
                    return hit.gameObject;
                }
            }
        }

        
        return null;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null)
        {
            return clickedObject.transform;
        }

        return null;
    }


}
