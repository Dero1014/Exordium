using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInteraction : MonoBehaviour
{
    //PUBLIC
    public InventoryDisplay InvDisplay;
    public EquipDisplay EDisplay;

    [Space(10)]
    public TooltipWindow ToolTip;
    public GameObject Prefab;

    [Space(10)]
    public GameObject[] UIs;


    //PRIVATE
    private PlayerAttributes _pAttributes; //make a SO for attributes;
    private SlotComponent _slotOfTheObjHolder;


    private Image _objectToDragImage;

    private Transform _objectToDrag;
    private Transform _target;

    private List<RaycastResult> _hitObjects = new List<RaycastResult>(); //saves all of the raycast results under the mouse

    private Vector2 _originalPosition;

    private bool _dragging = false;
    private bool _clicked = false; //clicked is used to have the ability to click and pickup or place an item in inventory


    private void Start()
    {
        _pAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
    }

    bool _itsOnInventory = false;
    bool _itsOnEquipment = false;

    void Update()
    {
        //!!!!!! set somewhere in the code equipment over inventory and inventory over equipment when selected !!!!!!

        print("So whats the child index " + InvDisplay.gameObject.transform.GetSiblingIndex());

        if (!UIs[0].activeSelf && !UIs[1].activeSelf)
        {
            if (_objectToDrag != null)
            {

                _objectToDrag.position = _originalPosition;
                _slotOfTheObjHolder._occupied = true;

                _objectToDragImage.raycastTarget = true;
                _objectToDrag = null;
            }

            _dragging = false;
            _clicked = false;
        }

        #region hover
        if (GetDraggableTransformUnderMouse())
        {
            _target = GetDraggableTransformUnderMouse();
            if (_target != null)
            {
                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Inventory_Slot newSlot = InvDisplay.objToItems[_target.gameObject];

                    GameObject clone = Instantiate(Prefab, GameObject.FindObjectOfType<PlayerInput>().gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                    clone.GetComponent<ItemComponent>().ItemType = InvDisplay.objToItems[_target.gameObject].Item;
                    clone.GetComponent<ItemComponent>().Amount = InvDisplay.objToItems[_target.gameObject].Amount;

                    GameObject remember = _target.gameObject;
                    InvDisplay.objToItems.Remove(_target.gameObject);
                    InvDisplay.itemsDisplayed.Remove(newSlot);
                    Destroy(remember);
                    InvDisplay.inventory.Container.Remove(newSlot);

                    _slotOfTheObjHolder._occupied = false;
                }


                ToolTip.Panel.SetActive(true);
                Inventory_Slot slot;


                if (InvDisplay.objToItems.ContainsKey(_target.gameObject))
                {
                    slot = InvDisplay.objToItems[_target.gameObject];
                    ToolTip.Slot = slot;
                }
                else if (EDisplay.ObjToEquipment.ContainsKey(_target.gameObject))
                {
                    slot = EDisplay.ObjToEquipment[_target.gameObject];
                    ToolTip.Slot = slot;
                }
                else
                {
                    ToolTip.Slot = null;
                }
            }
            else
            {
                ToolTip.Slot = null;
            }
        }
        else
        {
            ToolTip.Slot = null;
            ToolTip.Panel.SetActive(false);
        }

        #endregion

        #region RightClick equip

        if (Input.GetMouseButtonDown(1) && !_dragging)
        {
            //later try doing this with iDs

            _target = GetDraggableTransformUnderMouse();
            Inventory_Slot slot;

            bool equiped = false;

            if (_target != null)
            {
                if (InvDisplay.objToItems.ContainsKey(_target.gameObject))
                {
                    slot = InvDisplay.objToItems[_target.gameObject];

                    //Move the item from inventory to equip
                    for (int i = 0; i < EDisplay.Equipment.Container.Count; i++)
                    {
                        if (slot.Item.EquipTypes == EDisplay.Equipment.Container[i].AllowedEquip[0])
                        {
                            if (EDisplay.Equipment.Container[i].Item == null)
                            {
                                EDisplay.Equipment.Container[i].Item = slot.Item;
                                EDisplay.Equipment.Container[i].Amount = slot.Amount;
                                equiped = true;

                                break;
                            }
                        }
                    }

                    if (!equiped)
                    {
                        for (int i = 0; i < EDisplay.Equipment.Container.Count; i++)
                        {
                            if (slot.Item.EquipTypes == EDisplay.Equipment.Container[i].AllowedEquip[0])
                            {
                                if (EDisplay.Equipment.Container[i].Item != null)
                                {

                                    //remember the game object 
                                    GameObject remember = EDisplay.EquipDisplayStorage[EDisplay.Equipment.Container[i]];

                                    EDisplay.EquipDisplayStorage.Remove(EDisplay.ObjToEquipment[EDisplay.EquipDisplayStorage[EDisplay.Equipment.Container[i]]]);

                                    InvDisplay.inventory.AddItem(EDisplay.Equipment.Container[i].Item, EDisplay.Equipment.Container[i].Amount);
                                    Destroy(remember);

                                    EDisplay.Equipment.Container[i].Item = slot.Item;
                                    EDisplay.Equipment.Container[i].Amount = slot.Amount;
                                    break;
                                }

                            }
                        }
                    }

                    //now remove that item from the inventory
                    if (slot.Item.Type == ItemType.Equipable)
                    {
                        _slotOfTheObjHolder._occupied = false;
                        InvDisplay.objToItems.Remove(_target.gameObject);
                        InvDisplay.itemsDisplayed.Remove(slot);
                        InvDisplay.inventory.Container.Remove(slot);
                        Destroy(_target.gameObject);
                    }

                }
                else if (EDisplay.ObjToEquipment.ContainsKey(_target.gameObject))
                {
                    slot = EDisplay.ObjToEquipment[_target.gameObject];
                    InvDisplay.inventory.AddItem(slot.Item, slot.Amount);
                    print("Removed");

                    //remove the fucker
                    EDisplay.ObjToEquipment.Remove(_target.gameObject);
                    EDisplay.EquipDisplayStorage.Remove(slot);
                    EDisplay.Equipment.Container[EDisplay.Equipment.Container.IndexOf(slot)].Item = null;
                    EDisplay.Equipment.Container[EDisplay.Equipment.Container.IndexOf(slot)].Amount = 0;
                    Destroy(_target.gameObject);

                }

            }

        }

        #endregion

        #region ConsumeItem

        if (Input.GetMouseButtonDown(2))
        {
            _target = GetDraggableTransformUnderMouse();
            if (_target != null)
            {
                Inventory_Slot slot;
                slot = InvDisplay.objToItems[_target.gameObject];

                if (slot.Item.Type == ItemType.Default && slot.Item.Buffs.Length > 0)
                {
                    slot.Amount--;
                    if (slot.Amount <= 0)
                    {
                        _slotOfTheObjHolder._occupied = false;
                    }
                }
            }


        }

        #endregion

        #region pick up and dragg
        if (_dragging)
        {
            _objectToDrag.position = Input.mousePosition;
            _clicked = true;
        }

        if (Input.GetMouseButtonDown(0) && !_dragging)
        {
            _objectToDrag = GetDraggableTransformUnderMouse();

            if (_objectToDrag != null)
            {
                _itsOnInventory = OnInvDisplay();
                _itsOnEquipment = OnEquipDisplay();

                _dragging = true;

                _objectToDrag.SetAsLastSibling();

                _originalPosition = _objectToDrag.position;
                _objectToDragImage = _objectToDrag.GetComponentInChildren<Image>();
                _objectToDragImage.raycastTarget = false;
                _slotOfTheObjHolder._occupied = false;
            }
        }



        if (Input.GetMouseButtonDown(0) && _clicked)
        {
            if (_objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();
                //Found an object
                if (objectToReplace != null)
                {
                    OrganizeItem(objectToReplace);
                }
                else
                {
                    if (EDisplay.ObjToEquipment.ContainsKey(_objectToDrag.gameObject))
                    {
                        Inventory_Slot slot = EDisplay.ObjToEquipment[_objectToDrag.gameObject];

                        GameObject clone = Instantiate(Prefab, GameObject.FindObjectOfType<PlayerInput>().gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                        clone.GetComponent<ItemComponent>().ItemType = EDisplay.ObjToEquipment[_objectToDrag.gameObject].Item;
                        clone.GetComponent<ItemComponent>().Amount = EDisplay.ObjToEquipment[_objectToDrag.gameObject].Amount;

                        GameObject remember = _objectToDrag.gameObject;
                        EDisplay.ObjToEquipment.Remove(_objectToDrag.gameObject);
                        EDisplay.EquipDisplayStorage.Remove(slot);
                        Destroy(remember);
                        EDisplay.Equipment.Container.Remove(slot);

                        _slotOfTheObjHolder._occupied = false;
                    }
                    else
                    {
                        Inventory_Slot slot = InvDisplay.objToItems[_objectToDrag.gameObject];

                        GameObject clone = Instantiate(Prefab, GameObject.FindObjectOfType<PlayerInput>().gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
                        clone.GetComponent<ItemComponent>().ItemType = InvDisplay.objToItems[_objectToDrag.gameObject].Item;
                        clone.GetComponent<ItemComponent>().Amount = InvDisplay.objToItems[_objectToDrag.gameObject].Amount;

                        GameObject remember = _objectToDrag.gameObject;
                        InvDisplay.objToItems.Remove(_objectToDrag.gameObject);
                        InvDisplay.itemsDisplayed.Remove(slot);
                        Destroy(remember);
                        InvDisplay.inventory.Container.Remove(slot);

                        _slotOfTheObjHolder._occupied = false;
                    }


                }

                _objectToDragImage.raycastTarget = true;
                _objectToDrag = null;
            }

            _dragging = false;
            _clicked = false;
        }



        #endregion

    }

    void OrganizeItem(Transform objToReplace)
    {
        //check where the picked up item came from
        if (InvDisplay.objToItems.ContainsKey(_objectToDrag.gameObject))
        {
            //check where we are leaving it
            _itsOnInventory = OnInvDisplay();
            _itsOnEquipment = OnEquipDisplay();

            if (_itsOnInventory)
            {
                //regulary do your thing
                if (objToReplace.GetComponent<SlotComponent>()) //first check if its over an empty slot
                {
                    _objectToDrag.position = objToReplace.position;
                    _slotOfTheObjHolder._occupied = false; //set the slot we took it from to unoccupied
                    objToReplace.GetComponent<SlotComponent>()._occupied = true;
                }
                else
                {
                    _objectToDrag.position = objToReplace.position;
                    objToReplace.position = _originalPosition;
                    _slotOfTheObjHolder._occupied = true; //because you are just switching out values in the slot they both stay occupied
                }

            }
            else if (_itsOnEquipment)
            {
                //so its on equipment now, time to mess about and see what we hit
                if (objToReplace.GetComponent<SlotComponent>()) //first check if its over an empty slot
                {
                    //check which type the slot is
                    int numOfSlot = EDisplay.EquipSlots.IndexOf(objToReplace.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot

                    //then compare it with the equip type
                    Inventory_Slot slot = InvDisplay.objToItems[_objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);

                }
                else
                {
                    //check which type the slot is
                    int numOfSlot = EDisplay.EquipSlots.IndexOf(objToReplace.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot
                    print(numOfSlot);

                    //then compare it with the equip type
                    Inventory_Slot slot = InvDisplay.objToItems[_objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);
                }
            }

        }

        if (EDisplay.ObjToEquipment.ContainsKey(_objectToDrag.gameObject))
        {
            print("It belongs to equipment");

            //check where we are leaving it
            _itsOnInventory = OnInvDisplay();
            _itsOnEquipment = OnEquipDisplay();

            if (_itsOnInventory)
            {
                int numOfSlot = EDisplay.EquipSlots.IndexOf(_objectToDrag.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot
                Inventory_Slot slot = EDisplay.ObjToEquipment[_objectToDrag.gameObject];
                InvDisplay.inventory.AddItem(EDisplay.Equipment.Container[numOfSlot].Item, EDisplay.Equipment.Container[numOfSlot].Amount);
                EDisplay.Equipment.Container[numOfSlot].Item = null;

                EDisplay.ObjToEquipment.Remove(_objectToDrag.gameObject);
                EDisplay.EquipDisplayStorage.Remove(slot);
                Destroy(_objectToDrag.gameObject);

                _objectToDrag.position = objToReplace.position;
                _slotOfTheObjHolder._occupied = false; //set the slot we took it from to unoccupied

            }
            else if (_itsOnEquipment)
            {
                //so its on equipment now, time to mess about and see what we hit
                if (objToReplace.GetComponent<SlotComponent>()) //first check if its over an empty slot
                {
                    //check which type the slot is
                    int numOfSlot = EDisplay.EquipSlots.IndexOf(objToReplace.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot

                    //then compare it with the equip type
                    Inventory_Slot slot = EDisplay.ObjToEquipment[_objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);

                }
                else
                {
                    //check which type the slot is
                    int numOfSlot = EDisplay.EquipSlots.IndexOf(objToReplace.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot
                    print(numOfSlot);

                    //then compare it with the equip type
                    Inventory_Slot slot = EDisplay.ObjToEquipment[_objectToDrag.gameObject];

                    DragItemInEquipSlot(slot, numOfSlot);
                }
            }
        }

    }

    void DragItemInEquipSlot(Inventory_Slot slot, int numOfSlot) //here is where we check what to do with the lot we are placing on
    {
        bool equiped = false;

        if (slot.Item.EquipTypes == EDisplay.Equipment.Container[numOfSlot].AllowedEquip[0])
        {
            if (EDisplay.Equipment.Container[numOfSlot].Item == null) //if its empty
            {
                EDisplay.Equipment.Container[numOfSlot].Item = slot.Item; //fill the equipment slot
                EDisplay.Equipment.Container[numOfSlot].Amount = slot.Amount;
                equiped = true;
            }
            else
            {
                //remember the game object we are replacing
                GameObject remember = EDisplay.EquipDisplayStorage[EDisplay.Equipment.Container[numOfSlot]];

                EDisplay.EquipDisplayStorage.Remove(EDisplay.ObjToEquipment[EDisplay.EquipDisplayStorage[EDisplay.Equipment.Container[numOfSlot]]]); //remove the current from the display

                InvDisplay.inventory.AddItem(EDisplay.Equipment.Container[numOfSlot].Item, EDisplay.Equipment.Container[numOfSlot].Amount); //and the one we replaced back to the inventory
                Destroy(remember); //destroy the object we are replacing

                EDisplay.Equipment.Container[numOfSlot].Item = slot.Item; //fill the equipment slot 
                EDisplay.Equipment.Container[numOfSlot].Amount = slot.Amount;
                equiped = true;
            }

        }
        else
        {
            //if the type doesn't match return the object
            _objectToDrag.position = _originalPosition;
            _slotOfTheObjHolder._occupied = true;
        }

        //if it fits remove it from inventory
        if (equiped)
        {
            InvDisplay.objToItems.Remove(_objectToDrag.gameObject);
            InvDisplay.itemsDisplayed.Remove(slot);
            InvDisplay.inventory.Container.Remove(slot);
            Destroy(_objectToDrag.gameObject);
        }

    }


    bool OnInvDisplay()
    {

        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, _hitObjects);

        if (_hitObjects.Count <= 0) return false;

        foreach (var hit in _hitObjects)
        {
            if (hit.gameObject.GetComponent<InventoryDisplay>())
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

        EventSystem.current.RaycastAll(pointer, _hitObjects);

        if (_hitObjects.Count <= 0) return false;

        foreach (var hit in _hitObjects)
        {
            if (hit.gameObject.GetComponent<EquipDisplay>())
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

        EventSystem.current.RaycastAll(pointer, _hitObjects);

        if (_hitObjects.Count <= 0) return null;

        if (_dragging)
        {
            foreach (var hit in _hitObjects)
            {
                if (hit.gameObject.GetComponent<ItemUiComponent>())
                {
                    return hit.gameObject;
                }
                if (hit.gameObject.GetComponent<SlotComponent>())
                {
                    return hit.gameObject;
                }
            }
        }
        else
        {
            foreach (var hit in _hitObjects)
            {
                if (hit.gameObject.GetComponent<ItemUiComponent>())
                {
                    foreach (var slotHold in _hitObjects)
                    {
                        if (slotHold.gameObject.GetComponent<SlotComponent>())
                        {
                            _slotOfTheObjHolder = slotHold.gameObject.GetComponent<SlotComponent>();
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
