using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Collision_Item : MonoBehaviour
{
    public Inventory_Object inventory;

    private Player_Attributes pAttributes;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Item_Component item = col.GetComponent<Item_Component>();

        if (item.itemType.type == ItemType.Permanent)
        {
            for (int i = 0; i < item.itemType.buffs.Length; i++)
            {
                //apply stats for permament
                pAttributes.attributes[(int)item.itemType.buffs[i].attribute] += item.itemType.buffs[i].value;
            }
        }

        if (item && inventory.container.Count < 32)
        {
            //here we update to inventory Ui
            print("Collected item");
            inventory.AddItem(item.itemType, item.amount);
        }

        Destroy(col.gameObject);

    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

}
