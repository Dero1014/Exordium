using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public InventoryObject Inventory;
    public InventoryObject Equipment;
    [Space(10)]
    public TriggerCollisionItem Trigger;
    public PredefinedSpatialProximity Psp;
    public ProxDirectionItemPicker ProxyDirection;
    public ProxyItemPicker Prox;
    [Space(10)]
    public GameObject Text;
    public PlayerAttributes PAttributes;

    void Start()
    {
        PAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
    }

    public void Update()
    {
        //pick type of pick up system
        if (Input.GetKeyDown("1"))
        {
            Trigger.enabled = false;
            Psp.enabled = true;
            ProxyDirection.enabled = false;
            Prox.enabled = false;
        }

        if (Input.GetKeyDown("2"))
        {
            Trigger.enabled = true;
            Psp.enabled = false;
            ProxyDirection.enabled = false;
            Prox.enabled = false;
        }

        if (Input.GetKeyDown("3"))
        {
            Trigger.enabled = false;
            Psp.enabled = false;
            ProxyDirection.enabled = true;
            Prox.enabled = false;
        }

        if (Input.GetKeyDown("4"))
        {
            Trigger.enabled = false;
            Psp.enabled = false;
            ProxyDirection.enabled = false;
            Prox.enabled = true;
        }

        if (Inventory.Container.Count >= Inventory.Capacity)
            Text.SetActive(true);
        else
            Text.SetActive(false);

    }

    public void PickedUp(ItemComponent item) //every pick up system calls this to decide what to do with it and destroy it
    {
        if (item.ItemType.Type == ItemType.Permanent) //apply the permanent object
        {
            for (int i = 0; i < item.ItemType.Buffs.Length; i++)
            {
                //apply stats for permament
                Debug.Log("You picked up " + item.ItemType.ItemName);
                PAttributes.PickedUpItems.Add(item.ItemType);
                Destroy(item.gameObject);
            }

        }

        if (item && Inventory.Container.Count < Inventory.Capacity && item.ItemType.Type != ItemType.Permanent) //apply the permanent object
        {
            //here we update to inventory Ui
            Inventory.AddItem(item.ItemType, item.Amount);
            Destroy(item.gameObject);
        }
        else if (item && Inventory.Container.Count >= Inventory.Capacity && item.ItemType.Type != ItemType.Permanent)
        {
            print("You are full");
        }
    }

    private void OnApplicationQuit()
    {
        //control equipment size
        Inventory.Container.Clear();
        for (int i = 0; i < 3; i++)
        {
            Equipment.Container[i].Item = null;
        }
    }
}