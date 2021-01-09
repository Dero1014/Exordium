using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplitStackWindow : MonoBehaviour
{

    //public
    public InventoryObject inventory;
    [Space(10)]
    public GameObject Panel;
    [HideInInspector] public InventorySlot Slot;

    public TMP_InputField AmountText;
    public TextMeshProUGUI NameText;

    //private
    private int _splitAmount;

    private RectTransform _panelRectTransform;


    void Start()
    {
        _panelRectTransform = Panel.transform.GetComponent<RectTransform>();
    }

    private void Update()
    {

        if (AmountText.isFocused)
        {
            _splitAmount = int.Parse(AmountText.text);
            print(_splitAmount);
        }

        if (_splitAmount >= Slot.Amount)
        {
            _splitAmount = Slot.Amount - 1;
            UpdateText();
        }

        if (_splitAmount <= 0)
        {
            _splitAmount = 1;
            UpdateText();
        }

    }

    public void PositionWindow()
    {
        _panelRectTransform.position = Input.mousePosition + new Vector3(-75, +40, 0);
    }

    public void AssignText()
    {
        NameText.text = Slot.Item.ItemName;
        _splitAmount = Slot.Amount-1;
        AmountText.text = _splitAmount.ToString();
    }

    public void UpdateText()
    {
        AmountText.text = _splitAmount.ToString();

    }

    //buttons
    public void CloseWindow()
    {
        Panel.SetActive(false);
    }

    public void MinusOne() 
    {
        _splitAmount = int.Parse(AmountText.text);

        if (_splitAmount > 1)
            _splitAmount -= 1;

        UpdateText();
    }

    public void PlusOne()
    {
        _splitAmount = int.Parse(AmountText.text);

        if (_splitAmount<Slot.Amount-1)
            _splitAmount += 1;

        UpdateText();
    }

    public void OK()
    {
        inventory.SplitItems(Slot, _splitAmount, Slot.Durrability);

    }

}
