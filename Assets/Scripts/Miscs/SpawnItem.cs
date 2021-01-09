using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject Obj;
    public InventoryObject Items;

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

            GameObject clone = Instantiate(Obj, mosPos, Quaternion.identity);
            ItemComponent item = clone.GetComponent<ItemComponent>();

            item.ItemObject = Items.Container[Random.Range(0,Items.Container.Count)].Item;

            if (item.ItemObject.Type != ItemType.Equipable && item.ItemObject.Type != ItemType.Permanent)
            {
                item.Amount = Random.Range(1, 10);
            }


            if (item.ItemObject.Type == ItemType.Equipable)
            {
                item.Durrability = 100;
            }

        }
    }
}
