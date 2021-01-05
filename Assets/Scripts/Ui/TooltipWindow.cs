using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipWindow : MonoBehaviour
{
    public GameObject Panel;
    public Inventory_Slot Slot;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Buffs;

    public string[] TypeTexts;
    public string[] BuffTexts;

   
    void Update()
    {
        Panel.transform.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(0, - 30, 0);

        if (Slot!=null && Panel.activeSelf)
        {
            if (Slot.Item != null)
            {
                NameText.text = Slot.Item.ItemName;

                for (int i = 0; i < TypeTexts.Length; i++)
                {
                    Type.text = TypeTexts[(int)Slot.Item.Type];
                }

                if (Slot.Item.Buffs.Length > 0)
                {
                    Buffs.text = null;

                    Buffs.text = BuffTexts[(int)Slot.Item.Buffs[0].Attribute] + " " + Slot.Item.Buffs[0].Value.ToString() + "\n";
                    for (int i = 0; i < Slot.Item.Buffs.Length; i++)
                    {

                        Buffs.text += BuffTexts[(int)Slot.Item.Buffs[i].Attribute] + " " + Slot.Item.Buffs[i].Value.ToString() + "\n";
                    }
                }
            }
            else
            {
                NameText.text = null;
                Type.text = null;
                Buffs.text = null;
            }
        }
        
    }
}
