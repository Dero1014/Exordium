using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUIInteraction : MonoBehaviour
{
    //public
    public GameObject InventoryUI;
    public GameObject InventoryButton;
    [Space(10)]
    public GameObject EquipUI;
    public GameObject EquipButton;
    [Space(10)]
    public GameObject AttributeUI;
    public GameObject AttributeButton;

    //private

    private bool _inventoryOpen = false; //check condition for inventory
    private bool _equipOpen = false; //check condition for equip
    private bool _attributeOpen = false; //check condition for equip

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("i")) //input for inventory
        {
            if (_inventoryOpen)
                CloseInventory();
            else
                OpenInventory();
        }

        if (Input.GetKeyDown("e")) //input for equip
        {
            if (_equipOpen)
                CloseEquip();
            else
                OpenEquip();
        }

        if (Input.GetKeyDown("c")) //input for equip
        {
            if (_attributeOpen)
                CloseAttr();
            else
                OpenAttr();
        }
    }

    //actions to control opening and closing ui elements
    #region button actions 

    public void OpenInventory()
    {
        InventoryUI.SetActive(true);
        InventoryButton.SetActive(false);
        _inventoryOpen = true;
    }

    public void CloseInventory()
    {
        InventoryUI.SetActive(false);
        InventoryButton.SetActive(true);
        _inventoryOpen = false;
    }

    public void OpenEquip()
    {
        EquipUI.SetActive(true);
        EquipButton.SetActive(false);
        _equipOpen = true;
    }

    public void CloseEquip()
    {
        EquipUI.SetActive(false);
        EquipButton.SetActive(true);
        _equipOpen = false;
    }

    public void OpenAttr()
    {
        AttributeUI.SetActive(true);
        AttributeButton.SetActive(false);
        _attributeOpen = true;
    }

    public void CloseAttr()
    {
        AttributeUI.SetActive(false);
        AttributeButton.SetActive(true);
        _attributeOpen = false;
    }

    #endregion

}
