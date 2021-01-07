using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public InventoryObject Inventory;
    public InventoryObject Equipment;
    [Space(10)]
    public TriggerCollisionItem TriggerPickUp;
    public PredefinedSpatialProximity MouseClickPickUp;
    public ProxDirectionItemPicker ProxyDirectionPickUp;
    public ProxyItemPicker ProximityPickup;
    [Space(10)]
    public GameObject Text;

    //private
    private delegate void UpdateAttribute();

    private UpdateAttribute _updateAttributeValues;

    private PlayerAttributes _pAttributes;


    void Start()
    {
        _pAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
        _updateAttributeValues = _pAttributes.UpdateAttributes;
    }

    public void Update()
    {
        //pick type of pick up system
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetPickUp(true, false, false, false);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SetPickUp(false, true, false, false);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SetPickUp(false, false, true, false);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            SetPickUp(false, false, false, true);
        }

        if (Inventory.Container.Count >= Inventory.Capacity)
            Text.SetActive(true);
        else
            Text.SetActive(false);

    }

    void SetPickUp(bool trigger, bool mouse, bool proxyDirection, bool proxyPickUp)
    {
        TriggerPickUp.enabled = trigger;
        MouseClickPickUp.enabled = mouse;
        ProxyDirectionPickUp.enabled = proxyDirection;
        ProximityPickup.enabled = proxyPickUp;
    }


    public void PickedUp(ItemComponent item) //every pick up system calls this to decide what to do with it and destroy it
    {
        if (item.ItemObject.Type == ItemType.Permanent) //apply the permanent object
        {
            for (int i = 0; i < item.ItemObject.Buffs.Length; i++)
            {
                //apply stats for permament
                Debug.Log("You picked up " + item.ItemObject.ItemName);
                _pAttributes.PickedUpItems.Add(item.ItemObject);
                _updateAttributeValues();
                Destroy(item.gameObject);
            }

        }

        if (item && Inventory.Container.Count < Inventory.Capacity && item.ItemObject.Type != ItemType.Permanent) //apply the permanent object
        {
            //here we update to inventory Ui
            Debug.Log("You picked up " + item.ItemObject.ItemName);
            Inventory.AddItem(item.ItemObject, item.Amount, item.Durrability);
            Destroy(item.gameObject);
        }
        else if (item && Inventory.Container.Count >= Inventory.Capacity && item.ItemObject.Type != ItemType.Permanent)
        {
            print("You are full");
        }
    }

    private void OnApplicationQuit()
    {
        //control equipment size
        Inventory.Container.Clear();
        for (int i = 0; i < 7; i++)
        {
            Equipment.Container[i].Item = null;
        }
    }
}