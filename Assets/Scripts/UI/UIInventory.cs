using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;


    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    public GameObject useButton;
    public GameObject dropButton;

    private PlayerController controller;
    private PlayerCondition condition;

    
    ItemData selectedItem;
    int selectedItemIndex = 0;

    void Start()
    {
        Managers.Char.KeyActionStarted -= KeyAction_UIInventoryToggle;
        Managers.Char.KeyActionStarted += KeyAction_UIInventoryToggle;
        Managers.Char.KeyActionStarted -= KeyAction_AddItem;
        Managers.Char.KeyActionStarted += KeyAction_AddItem;


        controller = Managers.Char.Player.controller;
        condition = Managers.Char.Player.condition;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }

    }

    void KeyAction_AddItem()
    {
        ItemData data = Managers.Char.Player.itemData;

            //아이템이 중복 가능 한지 canStack
            if (data.canStack)
            {
                ItemSlot slot = GetItemStack(data);
                if (slot != null)
                {
                    slot.quantity++;
                    UpdateUI();
                    Managers.Char.Player.itemData = null;
                    return;
                }
            }

            // 비어있는 슬롯을 가져온다.
            ItemSlot emptySlot = GetEmptySlot(data);

            // 있다면
            if (emptySlot != null)
            {
                emptySlot.item = data;
                emptySlot.quantity = 1;
                UpdateUI();
                Managers.Char.Player.itemData = null;
                return;
            }

            // 없다면
            ThrowItem(data);
            Managers.Char.Player.itemData = null;
        
    }

    ItemSlot GetItemStack(ItemData data)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }

        return null;
    }

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, Managers.Char.Player.transform.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        useButton.SetActive(false);
        dropButton.SetActive(false);
    }


    public void KeyAction_UIInventoryToggle()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (IsOpen())
            {
                inventoryWindow.SetActive(false);
            }
            else
            {
                inventoryWindow.SetActive(true);
            }
        }
        
        KeyAction_ToggleCursor();
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }


    void KeyAction_ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }

    
    public void SelectItem(int index)
    {
        if (slots[index].item == null)
        {
            return;
        }

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;


        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveSelectedItem();
    }

    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }
    
}
