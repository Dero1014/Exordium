using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tooltip_Window : MonoBehaviour
{
    public GameObject panel;
    public Inventory_Slot slot;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI type;
    public TextMeshProUGUI buffs;

    public string[] typeTexts;
    public string[] buffTexts;

    void Start()
    {
        
    }
    bool oneShot = false;
    void Update()
    {
        panel.transform.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(0, - 30, 0);

        if (slot!=null)
        {
            if (slot.item != null)
            {

                if (!oneShot)
                {
                    nameText.text = slot.item.itemName;

                    for (int i = 0; i < typeTexts.Length; i++)
                    {
                        type.text = typeTexts[(int)slot.item.type];
                    }

                    if (slot.item.buffs.Length > 0)
                    {
                        buffs.text = buffTexts[(int)slot.item.buffs[0].attribute] + " " + slot.item.buffs[0].value.ToString() + "\n";
                        for (int i = 0; i < slot.item.buffs.Length; i++)
                        {

                            buffs.text += buffTexts[(int)slot.item.buffs[i].attribute] + " " + slot.item.buffs[i].value.ToString() + "\n";
                        }
                    }

                    oneShot = true;
                }
            }
            else
            {
                nameText.text = null;
                type.text = null;
                buffs.text = null;
                oneShot = false;
            }
        }
        
    }
}
