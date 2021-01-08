using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipWindow : MonoBehaviour
{
    //public
    public GameObject Panel;
    [HideInInspector]public InventorySlot Slot;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Durrability;
    public TextMeshProUGUI Buffs;

    public string[] TypeTexts;
    public string[] BuffTexts;

    public bool Mobile = false;

    //private
    private RectTransform _panelRectTransform;

    private void Start()
    {
        _panelRectTransform = Panel.transform.GetComponent<RectTransform>();
    }

    void Update()
    {

        if (!Mobile)
        {
            _panelRectTransform.position = Input.mousePosition + new Vector3(0, -60, 0);
        }

        if (Slot != null && Panel.activeSelf)
        {
            if (Slot.Item != null)
            {
                NameText.text = Slot.Item.ItemName;
                Durrability.text = Slot.Durrability.ToString();

                for (int i = 0; i < TypeTexts.Length; i++)
                {
                    Type.text = TypeTexts[(int)Slot.Item.Type];
                }

                if (Slot.Item.Buffs.Length > 0)
                {
                    Buffs.text = null;

                    for (int i = 0; i < Slot.Item.Buffs.Length; i++)
                    {
                        Buffs.text += BuffTexts[(int)Slot.Item.Buffs[i].Attribute] + " " + Slot.Item.Buffs[i].Value.ToString() + "\n";
                    }
                }
                else
                {
                    Buffs.text = "None";
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

    public void Reposition()
    {
        _panelRectTransform.position = Input.mousePosition + new Vector3(0, -60, 0);
    }

}
