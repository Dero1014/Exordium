using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Collision_Item : MonoBehaviour
{
    public Inventory_Object inventory;
    public Inventory_Object equipment;

    private Player_Attributes pAttributes;

    void Start()
    {
        pAttributes = GameObject.FindObjectOfType<Player_Attributes>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Item_Component item = col.GetComponent<Item_Component>();

        if (item.itemType.type == ItemType.Permanent) //apply the permanent object
        {
            for (int i = 0; i < item.itemType.buffs.Length; i++)
            {
                //apply stats for permament
                pAttributes.attributes[(int)item.itemType.buffs[i].attribute] += item.itemType.buffs[i].value;
            }

        }

        if (item && inventory.container.Count < 32 && item.itemType.type != ItemType.Permanent) //apply the permanent object
        {
            //here we update to inventory Ui
            print("Collected item");
            inventory.AddItem(item.itemType, item.amount);
        }

        Destroy(item.gameObject);

    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
        for (int i = 0; i < 4; i++)
        {
            equipment.container[i].item = null;
        }
    }

}
