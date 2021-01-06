using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipWindow : MonoBehaviour
{
    //public
    public GameObject Panel;
    [HideInInspector]public Inventory_Slot Slot;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Buffs;

    public string[] TypeTexts;
    public string[] BuffTexts;

    private RectTransform _panelRectTransform;
    private ItemBaseObject _slotItem;
    private ItemBuff[] _slotItemBuffs;

    private void Start()
    {
        _panelRectTransform = Panel.transform.GetComponent<RectTransform>();
        _slotItem = Slot.Item;
        _slotItemBuffs = _slotItem.Buffs;
    }

    void Update()
    {
        _panelRectTransform.position = Input.mousePosition + new Vector3(0, - 30, 0);

        if (Slot!=null && Panel.activeSelf)
        {
            if (Slot.Item != null)
            {
                NameText.text = _slotItem.ItemName;

                for (int i = 0; i < TypeTexts.Length; i++)
                {
                    Type.text = TypeTexts[(int)_slotItem.Type];
                }

                if (_slotItemBuffs.Length > 0)
                {
                    Buffs.text = null;

                    for (int i = 0; i < _slotItemBuffs.Length; i++)
                    {
                        Buffs.text += BuffTexts[(int)_slotItemBuffs[i].Attribute] + " " + _slotItemBuffs[i].Value.ToString() + "\n";
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
}
