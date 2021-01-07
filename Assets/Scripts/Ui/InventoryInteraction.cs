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
    public SplitStackWindow SplitStack; 

    public GameObject Prefab;

    [Space(10)]
    public GameObject[] UIs;

    //PRIVATE
    private delegate void UpdateEquip();
    private delegate void UpdateAttribute();

    private UpdateEquip _updateEquipLoadout;
    private UpdateAttribute _updateAttributeValues;

    private PlayerAttributes _pAttributes; //make a SO for attributes;
    private SlotComponent _slotOfTheObjHolder;

    private Image _objectToDragImage;

    private Transform _objectToDrag;
    private Transform _target;

    private List<RaycastResult> _hitObjects = new List<RaycastResult>(); //saves all of the raycast results under the mouse

    private Vector2 _originalPosition;

    private bool _dragging = false;
    private bool _clicked = false; //clicked is used to have the ability to click and pickup or place an item in inventory

    private Transform _playerPosition;
    private GameObject _indexObject;
    private void Awake()
    {
        _playerPosition = GameObject.FindObjectOfType<PlayerInput>().gameObject.transform;
    }

    private void Start()
    {
        _pAttributes = GameObject.FindObjectOfType<PlayerAttributes>();

        _updateAttributeValues = _pAttributes.UpdateAttributes;
        _updateEquipLoadout = EDisplay.UpdateDisplay;
    }

    bool _itsOnInventory = false;
    bool _itsOnEquipment = false;
    bool equiped = false;


    void Update()
    {
        //!!!!!! set somewhere in the code equipment over inventory and inventory over equipment when selected !!!!!!
        equiped = false;

        if (!UIs[0].activeSelf && !UIs[1].activeSelf && _objectToDrag != null)
        {
            _objectToDrag.position = _originalPosition;
            _slotOfTheObjHolder._occupied = true;

            _objectToDragImage.raycastTarget = true;
            _objectToDrag = null;

            _dragging = false;
            _clicked = false;
        }

        #region hover
        if (GetDraggableTransformUnderMouse())
        {
            _target = GetDraggableTransformUnderMouse();
            Inventory_Slot slot;

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Inventory_Slot newSlot = InvDisplay.ObjToItems[_target.gameObject];

                GameObject clone = Instantiate(Prefab, _playerPosition.position + new Vector3(0, -3, 0), Quaternion.identity);
                clone.GetComponent<ItemComponent>().ItemType = InvDisplay.ObjToItems[_target.gameObject].Item;
                clone.GetComponent<ItemComponent>().Amount = InvDisplay.ObjToItems[_target.gameObject].Amount;

                GameObject objToRemember = _target.gameObject;
                InvDisplay.ObjToItems.Remove(_target.gameObject);
                InvDisplay.ItemsDisplayed.Remove(newSlot);
                Destroy(objToRemember);
                InvDisplay.Inventory.Container.Remove(newSlot);

                _slotOfTheObjHolder._occupied = false;
            }

            //TOOLTIP
            ToolTip.Panel.SetActive(true);

            if (InvDisplay.ObjToItems.ContainsKey(_target.gameObject))
            {
                slot = InvDisplay.ObjToItems[_target.gameObject];
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

            //SPLIT STACK
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                SplitStack.Panel.SetActive(true);
                SplitStack.PositionWindow();
                if (InvDisplay.ObjToItems.ContainsKey(_target.gameObject))
                {
                    slot = InvDisplay.ObjToItems[_target.gameObject];
                    SplitStack.Slot = slot;
                    SplitStack.AssignText();
                }
                else
                {
                    SplitStack.Slot = null;
                }

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


            if (_target != null)
            {
                if (InvDisplay.ObjToItems.ContainsKey(_target.gameObject))
                {
                    var equipContainerCount = EDisplay.Equipment.Container.Count;
                    slot = InvDisplay.ObjToItems[_target.gameObject];

                    //Move the item from inventory to equip
                    for (int i = 0; i < equipContainerCount; i++)
                    {
                        var allowedType = EDisplay.Equipment.Container[i].AllowedEquip[0];

                        if (slot.Item.EquipTypes == allowedType)
                        {
                            if (EDisplay.Equipment.Container[i].Item == null)
                            {
                                EDisplay.Equipment.Container[i].Item = slot.Item;
                                EDisplay.Equipment.Container[i].Amount = slot.Amount;
                                equiped = true;

                                break;
                            }

                            if (EDisplay.Equipment.Container[i].Item != null)
                            {

                                //remember the game object 
                                var containerSlot = EDisplay.Equipment.Container[i];
                                var objToRemember = EDisplay.EquipDisplayStorage[containerSlot];

                                EDisplay.EquipDisplayStorage.Remove(EDisplay.ObjToEquipment[objToRemember]);

                                InvDisplay.Inventory.AddItem(EDisplay.Equipment.Container[i].Item, EDisplay.Equipment.Container[i].Amount);
                                Destroy(objToRemember);

                                EDisplay.Equipment.Container[i].Item = slot.Item;
                                EDisplay.Equipment.Container[i].Amount = slot.Amount;
                                break;
                            }

                        }
                    }


                    //now remove that item from the inventory
                    if (slot.Item.Type == ItemType.Equipable)
                    {
                        _slotOfTheObjHolder._occupied = false;
                        InvDisplay.ObjToItems.Remove(_target.gameObject);
                        InvDisplay.ItemsDisplayed.Remove(slot);
                        InvDisplay.Inventory.Container.Remove(slot);
                        Destroy(_target.gameObject);
                    }

                }
                else if (EDisplay.ObjToEquipment.ContainsKey(_target.gameObject))
                {
                    slot = EDisplay.ObjToEquipment[_target.gameObject];
                    var containerSlot = EDisplay.Equipment.Container.IndexOf(slot);

                    InvDisplay.Inventory.AddItem(slot.Item, slot.Amount);

                    //remove the fucker
                    EDisplay.ObjToEquipment.Remove(_target.gameObject);
                    EDisplay.EquipDisplayStorage.Remove(slot);
                    EDisplay.Equipment.Container[containerSlot].Item = null;
                    EDisplay.Equipment.Container[containerSlot].Amount = 0;
                    Destroy(_target.gameObject);

                }

            }
            _updateAttributeValues();
            _updateEquipLoadout();

        }

        #endregion

        #region ConsumeItem

        if (Input.GetMouseButtonDown(2))
        {
            _target = GetDraggableTransformUnderMouse();
            if (_target != null)
            {
                Inventory_Slot slot;
                slot = InvDisplay.ObjToItems[_target.gameObject];

                if (slot.Item.Type == ItemType.Default && slot.Item.Buffs.Length > 0)
                {
                    slot.Amount--;
                    if (slot.Amount <= 0) _slotOfTheObjHolder._occupied = false;

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
                _indexObject = _objectToDrag.gameObject;
                //Found an object
                if (objectToReplace != null)
                {
                    OrganizeItem(objectToReplace);
                }
                else
                {
                    if (EDisplay.ObjToEquipment.ContainsKey(_indexObject))
                    {
                        
                        Inventory_Slot slot = EDisplay.ObjToEquipment[_indexObject];

                        GameObject clone = Instantiate(Prefab, _playerPosition.position + new Vector3(0, -3, 0), Quaternion.identity);
                        clone.GetComponent<ItemComponent>().ItemType = EDisplay.ObjToEquipment[_indexObject].Item;
                        clone.GetComponent<ItemComponent>().Amount = EDisplay.ObjToEquipment[_indexObject].Amount;

                 
                        EDisplay.ObjToEquipment.Remove(_indexObject);
                        EDisplay.EquipDisplayStorage.Remove(slot);
                        Destroy(_indexObject);
                        EDisplay.Equipment.Container.Remove(slot);

                    }
                    else
                    {
                        Inventory_Slot slot = InvDisplay.ObjToItems[_indexObject];

                        GameObject clone = Instantiate(Prefab, _playerPosition.position + new Vector3(0, -3, 0), Quaternion.identity);
                        clone.GetComponent<ItemComponent>().ItemType = InvDisplay.ObjToItems[_indexObject].Item;
                        clone.GetComponent<ItemComponent>().Amount = InvDisplay.ObjToItems[_indexObject].Amount;

           
                        InvDisplay.ObjToItems.Remove(_indexObject);
                        InvDisplay.ItemsDisplayed.Remove(slot);
                        Destroy(_indexObject);
                        InvDisplay.Inventory.Container.Remove(slot);

                    }

                    _slotOfTheObjHolder._occupied = false;
                    _slotOfTheObjHolder._occupied = false;

                }

                _objectToDragImage.raycastTarget = true;
                _objectToDrag = null;
            }

            _dragging = false;
            _clicked = false;
            _updateEquipLoadout();
            _updateAttributeValues();
        }



        #endregion


    }

    void OrganizeItem(Transform objToReplace)
    {
        //check where the picked up item came from
        if (InvDisplay.ObjToItems.ContainsKey(_indexObject))
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
                    Inventory_Slot slot = InvDisplay.ObjToItems[_indexObject];

                    DragItemInEquipSlot(slot, numOfSlot);

                }
                else
                {
                    //check which type the slot is
                    int numOfSlot = EDisplay.EquipSlots.IndexOf(objToReplace.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot
                    print(numOfSlot);

                    //then compare it with the equip type
                    Inventory_Slot slot = InvDisplay.ObjToItems[_indexObject];

                    DragItemInEquipSlot(slot, numOfSlot);
                }
            }

        }

        if (EDisplay.ObjToEquipment.ContainsKey(_indexObject))
        {

            //check where we are leaving it
            _itsOnInventory = OnInvDisplay();
            _itsOnEquipment = OnEquipDisplay();

            if (_itsOnInventory)
            {
                int numOfSlot = EDisplay.EquipSlots.IndexOf(_objectToDrag.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot
                Inventory_Slot slot = EDisplay.ObjToEquipment[_indexObject];
                InvDisplay.Inventory.AddItem(EDisplay.Equipment.Container[numOfSlot].Item, EDisplay.Equipment.Container[numOfSlot].Amount);
                EDisplay.Equipment.Container[numOfSlot].Item = null;

                EDisplay.ObjToEquipment.Remove(_indexObject);
                EDisplay.EquipDisplayStorage.Remove(slot);
                Destroy(_indexObject);

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
                    Inventory_Slot slot = EDisplay.ObjToEquipment[_indexObject];

                    DragItemInEquipSlot(slot, numOfSlot);

                }
                else
                {
                    //check which type the slot is
                    int numOfSlot = EDisplay.EquipSlots.IndexOf(objToReplace.GetComponentInParent<SlotComponent>().transform); //first get the number of that slot
                    print(numOfSlot);

                    //then compare it with the equip type
                    Inventory_Slot slot = EDisplay.ObjToEquipment[_indexObject];

                    DragItemInEquipSlot(slot, numOfSlot);
                }
            }
        }

    }

    void DragItemInEquipSlot(Inventory_Slot slot, int numOfSlot) //here is where we check what to do with the lot we are placing on
    {
        var containerSlot = EDisplay.Equipment.Container[numOfSlot];

        if (slot.Item.EquipTypes == containerSlot.AllowedEquip[0])
        {
            if (containerSlot.Item == null) //if its empty
            {
                containerSlot.Item = slot.Item; //fill the equipment slot
                containerSlot.Amount = slot.Amount;
            }
            else
            {
                //remember the game object we are replacing
                GameObject objToRemember = EDisplay.EquipDisplayStorage[containerSlot];

                EDisplay.EquipDisplayStorage.Remove(EDisplay.ObjToEquipment[EDisplay.EquipDisplayStorage[containerSlot]]); //remove the current from the display

                InvDisplay.Inventory.AddItem(containerSlot.Item, containerSlot.Amount); //and the one we replaced back to the inventory
                Destroy(objToRemember); //destroy the object we are replacing

                containerSlot.Item = slot.Item; //fill the equipment slot 
                containerSlot.Amount = slot.Amount;
            }
            equiped = true;

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
            InvDisplay.ObjToItems.Remove(_indexObject);
            InvDisplay.ItemsDisplayed.Remove(slot);
            InvDisplay.Inventory.Container.Remove(slot);
            Destroy(_indexObject);
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
