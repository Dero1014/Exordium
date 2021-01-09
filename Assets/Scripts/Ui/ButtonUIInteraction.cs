using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;


class Window
{
    private GameObject _window;

    public GameObject WindowUI
    {
        get
        {
            return _window;
        }set
        {
            _window = value;
        }

    }
    public void OpenClose()
    {
        _window.SetActive(!_window.activeSelf);
        WindowOpened(_window);
    }

    void WindowOpened(GameObject openedWindow)
    {
        if (openedWindow.activeSelf)
        {
            AnalyticsOnOpen(openedWindow);
        }
    }

    void AnalyticsOnOpen(GameObject window)
    {
        AnalyticsResult result = Analytics.CustomEvent("Items Equiped Up", new Dictionary<string, object>
        {
            { "Window ", window.name },
        });

    }

}

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
    private Window _inventory = new Window();
    private Window _equip = new Window();
    private Window _attributes = new Window();

    private void Start()
    {
        _inventory.WindowUI = InventoryUI;
        _equip.WindowUI = EquipUI;
        _attributes.WindowUI = AttributeUI;
    }

    void Update()
    {
        if (Input.GetKeyDown("i")) //input for inventory
        {
            InventoryWindow();
        }

        if (Input.GetKeyDown("e")) //input for equip
        {
            EquipWindow();
        }

        if (Input.GetKeyDown("c")) //input for equip
        {
            AttributeWindow();
        }


    }

    //actions to control opening and closing ui elements
    #region button actions 

    public void InventoryWindow()
    {
        _inventory.OpenClose();
        InventoryButton.SetActive(!InventoryButton.activeSelf);
    }

    public void EquipWindow()
    {
        _equip.OpenClose();
        EquipButton.SetActive(!EquipButton.activeSelf);
    }


    public void AttributeWindow()
    {
        _attributes.OpenClose();
        AttributeButton.SetActive(!AttributeButton.activeSelf);
    }

    
    #endregion

}
