using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private GameObject slotPrefab;


    private List<Image> slotIcons = new List<Image>();
    private List<TextMeshProUGUI> slotTexts = new List<TextMeshProUGUI>();
    private List<Image> hotbarSlotIcons = new List<Image>();
    private List<TextMeshProUGUI> hotbarSlotTexts = new List<TextMeshProUGUI>();
    private List<GameObject> hotbarSlots = new List<GameObject>(); // ������ � ���������

    public event Action<ItemInstance> SlotChanged; //���� ��� ���� ���������

    private Vector3 normalScale = Vector3.one;
    private Vector3 selectedScale = Vector3.one * 1.15f;
    private int curSlot = 0;
    private int prevSlot = -1;
    void Awake()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<Inventory>();
            Debug.Log($"Inventory ������: {playerInventory.name}");
        }
        if (playerInventory == null)
        {
            Debug.LogError("Inventory dont founb");
            return;
        }
        inventoryPanel.SetActive(false);
    }
    void Start()
    {
        playerInventory.OnInventoryChanged += UpdateInventoryUI;
        createSlots();
        UpdateHotbarCursor();
        // UpdateInventoryUI();
        inventoryPanel.SetActive(false);
    }
    void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                curSlot = i;
                UpdateHotbarCursor();
                SlotChanged?.Invoke(playerInventory.items[i]);
                break;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerInventory.DropItem(curSlot))
            {
                SlotChanged?.Invoke(playerInventory.items[curSlot]);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            var item = playerInventory.items[curSlot];
            if (item != null)
            {
                item.UseRMB();
            }
        }
    }

    void createSlots()
    {
        for (int i = 0; i < playerInventory.capacity; i++)
        {
            
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
            Transform iconTransform = slot.transform.Find("Icon");
            Image icon = iconTransform?.GetComponent<Image>();
            TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
            slotIcons.Add(icon);
            slotTexts.Add(text);
            //Debug.Log($"���� {i} ������ �� {playerInventory.capacity}");
        }
        for (int i = 0; i < 9; i++)
        {

            GameObject slot = Instantiate(slotPrefab, hotbar.transform);
            Transform iconTransform = slot.transform.Find("Icon");
            Image icon = iconTransform?.GetComponent<Image>();
            TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
            hotbarSlots.Add(slot);
            hotbarSlotIcons.Add(icon);
            hotbarSlotTexts.Add(text);
            //Debug.Log($"���� {i} ������ �� {playerInventory.capacity}");
        }
    }
    private void UpdateInventoryUI()
    {
        Debug.Log("��������� �������� ����������"); // я не помню что это было. ебучая студия убила мне текст

        for (int i = 0; i < playerInventory.capacity; i++)
        {
            if (playerInventory.items[i] != null && playerInventory.items[i].itemTemplate != null)
            {
                slotIcons[i].sprite = playerInventory.items[i].itemTemplate.Icon;
                slotIcons[i].gameObject.SetActive(true);
                slotIcons[i].enabled = true;
                if (playerInventory.items[i].itemTemplate.IsStackable)
                {
                    slotTexts[i].text = playerInventory.items[i].count.ToString();
                }
                if (curSlot == i)
                {
                    SlotChanged?.Invoke(playerInventory.items[i]);
                }
            }
            else if (playerInventory.items[i] == null)
            {
                slotIcons[i].sprite = null;
                slotIcons[i].gameObject.SetActive(false);
                slotIcons[i].enabled = false;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (playerInventory.items[i] != null && playerInventory.items[i].itemTemplate != null)
            {
                hotbarSlotIcons[i].sprite = playerInventory.items[i].itemTemplate.Icon;
                hotbarSlotIcons[i].gameObject.SetActive(true);
                hotbarSlotIcons[i].enabled = true;
                if (playerInventory.items[i].itemTemplate.IsStackable)
                {
                    hotbarSlotTexts[i].text = playerInventory.items[i].count.ToString();
                }
            }
            else if (playerInventory.items[i] == null)
            {
                hotbarSlotIcons[i].sprite = null;
                hotbarSlotIcons[i].gameObject.SetActive(false);
                hotbarSlotIcons[i].enabled = false;
            }
        }
    }

    private void UpdateHotbarCursor()
    {
        // ���������, ��� � ������� ���� �������� �������
        if (hotbar.transform.childCount == 0)
        {
            Debug.LogWarning("� ������� ��� ������");
            return;
        }

        if (curSlot < 0 || curSlot >= hotbar.transform.childCount)
        {
            Debug.LogWarning($"curSlot ({curSlot}) ������� �� ������� (0-{hotbar.transform.childCount - 1})");
            return;
        }

        if (prevSlot >= 0 && prevSlot < hotbar.transform.childCount)
        {
            Transform slot = hotbar.transform.GetChild(prevSlot);
            slot.localScale = normalScale;
        }

        Transform currentSlot = hotbar.transform.GetChild(curSlot);
        currentSlot.localScale = selectedScale;

        prevSlot = curSlot;
    }


}
