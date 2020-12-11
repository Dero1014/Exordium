using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_UI_Interaction : MonoBehaviour
{
    //public
    public GameObject inventoryUI;
    public GameObject inventoryButton;
    [Space(10)]
    public GameObject equipUI;
    public GameObject equipButton;

    //private

    private bool inventoryOpen = false; //check condition for inventory
    private bool equipOpen = false; //check condition for equip

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("i")) //input for inventory
        {

            if (inventoryOpen)
                CloseInventory();
            else
                OpenInventory();

        }

        if (Input.GetKeyDown("e")) //input for equip
        {

            if (equipOpen)
                CloseEquip();
            else
                OpenEquip();

        }
    }

    //actions to control opening and closing ui elements
    #region button actions 

    public void OpenInventory()
    {
        print("It works");
        inventoryUI.SetActive(true);
        inventoryButton.SetActive(false);
        inventoryOpen = true;
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        inventoryButton.SetActive(true);
        inventoryOpen = false;
    }

    public void OpenEquip()
    {
        equipUI.SetActive(true);
        equipButton.SetActive(false);
        equipOpen = true;
    }

    public void CloseEquip()
    {
        equipUI.SetActive(false);
        equipButton.SetActive(true);
        equipOpen = false;
    }

    #endregion

}
