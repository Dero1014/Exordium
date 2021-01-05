using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Item : MonoBehaviour
{
    public GameObject obj;
    public Inventory_Object items;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Vector3 mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mosPos.z = 0;

            GameObject clone = Instantiate(obj, mosPos, Quaternion.identity);
            Item_Component item = clone.GetComponent<Item_Component>();

            item.itemType = items.container[Random.Range(0,items.container.Count)].item;

            if (item.itemType.type != ItemType.Equipable && item.itemType.type != ItemType.Permanent)
            {
                item.amount = Random.Range(1, 10);
            }


        }
    }
}
