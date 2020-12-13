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

    //PRIVATE
    private bool dragging = false;
    private bool clicked = false; //clicked is used to have the ability to click and pickup or place an item in inventory

    private Player_Attributes pAttributes; //make a SO for attributes;

    private Vector2 originalPosition;
    public Transform objectToDrag;
    private Image objectToDragImage;

    private Transform target;

    List<RaycastResult> hitObjects = new List<RaycastResult>(); //saves all of the raycast results under the mouse

    private void Start()
    {
        pAttributes = GameObject.FindObjectOfType<Player_Attributes>();
    }

    void Update()
    {

        #region RightClick mockery

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
                                    //eDisplay.equipDisplay.Clear();

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

        #region consumeeeeeeeeeeeee

        if (Input.GetMouseButtonDown(3))
        {
            target = GetDraggableTransformUnderMouse();
            Inventory_Slot slot;
            slot = iDisplay.objToItems[target.gameObject];
            print("Yis!");
            if (slot.item.type == ItemType.Default && slot.item.buffs.Length > 0) 
            {
                Debug.Log("U used an item");
                slot.amount--;
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
                dragging = true;

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponentInChildren<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        

        if (Input.GetMouseButtonDown(0) && clicked)
        {
            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null)
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = originalPosition;
                }
                else
                {
                    objectToDrag.position = originalPosition;
                }
               
                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
            clicked = false;
        }

    

        #endregion

    }

    //IEnumerator ApplyConsumable(Inventory_Slot slot, int at)
    //{
    //    pAttributes.attributes[(int)slot.item.buffs[at].attribute] += slot.item.buffs[at].value;
    //    print("start");
    //    yield return new WaitForSeconds(5);
    //    print("no");
    //    pAttributes.attributes[(int)slot.item.buffs[at].attribute] -= slot.item.buffs[at].value;
    //}

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        foreach (var hit in hitObjects)
        {
            if (hit.gameObject.GetComponent<Item_Ui_Component>())
            {
                return hit.gameObject;
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
