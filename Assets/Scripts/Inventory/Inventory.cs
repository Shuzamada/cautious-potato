using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;

    [SerializeField] public List<ItemInstance> items = new List<ItemInstance>();

    public int capacity = 27;


    [SerializeField] public GameObject emptyPrefab;
    [SerializeField] private Camera camera; //для дропа

    void Start()
    {
        camera = Camera.main;
        while (items.Count < capacity)
        {
            items.Add(null);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            bool full = true;
            for (int i = 0; i < capacity; i++)
            {
                if (items[i] == null)
                {
                    full = false;
                    break;
                }
            }
            if (full)
            {
                return;
            }
            PickupItem pickup = collision.gameObject.GetComponent<PickupItem>();
            AddItem(pickup.itemInstance);
            OnInventoryChanged?.Invoke();
            Debug.Log("предмет в инвентаре");
            Destroy(collision.gameObject);
        }
    }

    void AddItem(ItemInstance newItem)
    {
        int res = 0;
        for (int i = 0; i < capacity; i++)
        {
            if (items[i] != null && items[i].itemTemplate != null)
            {
                if (items[i].Id == newItem.Id && newItem.IsStackable)
                {
                    res = items[i].AddItem(ref newItem);
                    if (res == 0)
                    {
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < capacity; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                return;
            }
        }
    }

    public void Swap(int first, int second)
    {
        ItemInstance temp = items[first];
        items[first] = items[second];
        items[second] = temp;
    }

    public bool DropItem(int slot, bool all = false)
    {
        if (items[slot] == null) return false;

        // 1. Формируем луч от курсора
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 2. Вычисляем позицию спавна
        Vector3 spawnPos;
        if (Physics.Raycast(ray, out hit))                       // есть попадание
        {
            float dist = Vector3.Distance(ray.origin, hit.point);
            spawnPos = dist <= 5f ? hit.point : ray.origin + ray.direction.normalized * 5f;
        }
        else
        {
            spawnPos = ray.origin + ray.direction.normalized * 5f;
        }

        SpawnPickup(items[slot], spawnPos);
        RemoveItem(slot);
        // 4. Обновляем инвентарь
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(int slot)
    {
        if (items[slot] == null)
        {
            return false;
        }
        items[slot] = null;
        OnInventoryChanged?.Invoke();
        return true;

    }

    private void SpawnPickup(ItemInstance item, Vector3 pos)
    {
        if (item == null || item.ItemPrefab == null) return;
        GameObject drop = Instantiate(item.ItemPrefab, pos, Quaternion.identity);
    }   
}


