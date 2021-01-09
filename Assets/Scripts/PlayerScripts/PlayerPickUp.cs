using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
            SetPickUp(true, false, false, false);

        if (Input.GetKeyDown(KeyCode.F2))
            SetPickUp(false, true, false, false);

        if (Input.GetKeyDown(KeyCode.F3))
            SetPickUp(false, false, true, false);

        if (Input.GetKeyDown(KeyCode.F4))
            SetPickUp(false, false, false, true);

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
                //apply stats for permament types
                Debug.Log("You picked up " + item.ItemObject.ItemName);
                _pAttributes.PickedUpItems.Add(item.ItemObject);
                _updateAttributeValues();
                AnalyticsOnPickUp(item.ItemObject);
                Destroy(item.gameObject);
            }

        }

        if (item && Inventory.Container.Count < Inventory.Capacity && item.ItemObject.Type != ItemType.Permanent) //apply the permanent object
        {
            //here we update to inventory Ui
            Debug.Log("You picked up " + item.ItemObject.ItemName);
            Inventory.AddItem(item.ItemObject, item.Amount, item.Durrability);
            AnalyticsOnPickUp(item.ItemObject);
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

        if (Equipment.Container.Count < 7)
        {
            Equipment.Container.Add(null);
        }

        Equipment.Container[0].Item = null;
        Equipment.Container[0].Amount = 1;
        Equipment.Container[0].Durrability = 0;
        Equipment.Container[0].AllowedEquip = EquipType.Head;

        Equipment.Container[1].Item = null;
        Equipment.Container[1].Amount = 1;
        Equipment.Container[1].Durrability = 0;
        Equipment.Container[1].AllowedEquip = EquipType.Hands;

        Equipment.Container[2].Item = null;
        Equipment.Container[2].Amount = 1;
        Equipment.Container[2].Durrability = 0;
        Equipment.Container[2].AllowedEquip = EquipType.Shield;


        Equipment.Container[3].Item = null;
        Equipment.Container[3].Amount = 1;
        Equipment.Container[3].Durrability = 0;
        Equipment.Container[3].AllowedEquip = EquipType.Boots;

        Equipment.Container[4].Item = null;
        Equipment.Container[4].Amount = 1;
        Equipment.Container[4].Durrability = 0;
        Equipment.Container[4].AllowedEquip = EquipType.Ring;

        Equipment.Container[5].Item = null;
        Equipment.Container[5].Amount = 1;
        Equipment.Container[5].Durrability = 0;
        Equipment.Container[5].AllowedEquip = EquipType.Ring;

        Equipment.Container[6].Item = null;
        Equipment.Container[6].Amount = 1;
        Equipment.Container[6].Durrability = 0;
        Equipment.Container[6].AllowedEquip = EquipType.Chest;

       
    }

    void AnalyticsOnPickUp(ItemBaseObject item)
    {
        AnalyticsResult result = Analytics.CustomEvent("Items Picked Up", new Dictionary<string, object>
        {
            { "Item Name", item.ItemName },
            { "Item Type", item.Type }
        });

    }

}