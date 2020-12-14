using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PickUp : MonoBehaviour
{
    public Inventory_Object inventory;
    public Inventory_Object equipment;
    [Space(10)]
    public Trigger_Collision_Item trig;
    public Predefined_Spatial_Proximity psp;
    public Prox_Direction_ItemPicker proxy;
    public Proxy_ItemPicker prox;
    [Space(10)]
    public GameObject text;
    private Player_Attributes pAttributes;

    void Start()
    {
        pAttributes = GameObject.FindObjectOfType<Player_Attributes>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            trig.enabled = true;
            psp.enabled = false;
            proxy.enabled = false;
            prox.enabled = false;
        }

        if (Input.GetKeyDown("2"))
        {
            trig.enabled = false;
            psp.enabled = true;
            proxy.enabled = false;
            prox.enabled = false;
        }

        if (Input.GetKeyDown("3"))
        {
            trig.enabled = false;
            psp.enabled = false;
            proxy.enabled = true;
            prox.enabled = false;
        }

        if (Input.GetKeyDown("4"))
        {
            trig.enabled = false;
            psp.enabled = false;
            proxy.enabled = false;
            prox.enabled = true;
        }

        if (inventory.container.Count >= inventory.capacity)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }

    }

    public void PickedUp(Item_Component item)
    {
        if (item.itemType.type == ItemType.Permanent) //apply the permanent object
        {
            for (int i = 0; i < item.itemType.buffs.Length; i++)
            {
                //apply stats for permament
                Debug.Log("You picked up " + item.itemType.itemName);
                pAttributes.pickedUpItems.Add(item.itemType);
                Destroy(item.gameObject);

            }

        }

        if (item && inventory.container.Count < inventory.capacity && item.itemType.type != ItemType.Permanent) //apply the permanent object
        {
            //here we update to inventory Ui
            inventory.AddItem(item.itemType, item.amount);
            Destroy(item.gameObject);
        }
        else if (item && inventory.container.Count >= inventory.capacity && item.itemType.type != ItemType.Permanent)
        {
            print("You are full");
        }
    }


    private void OnApplicationQuit()
    {
        inventory.container.Clear();
        for (int i = 0; i < 3; i++)
        {
            equipment.container[i].item = null;
        }
    }
}
