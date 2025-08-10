using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class ItemInstance
{
    public ItemInterface itemTemplate;
    public int count;

    public int Id => itemTemplate.Id;
    public bool IsStackable => itemTemplate.IsStackable;
    public int MaxStackSize => itemTemplate.MaxStackSize;
    public GameObject ItemPrefab => itemTemplate.ItemPrefab;
    public ItemInterface ItemTemplate => itemTemplate;
    public ItemInterface ItemInterface => itemTemplate;
    public virtual void UseRMB() { }
    public virtual void UseLMB() { }
    public ItemInstance(ItemInterface itemTemplate, int count = 1)
    {
        this.itemTemplate = itemTemplate;
        this.count = count;
    }

    public bool CanStack(ItemInstance other)
    {
        return itemTemplate.IsStackable && itemTemplate == other.itemTemplate;
    }

    public int AddItem(ref ItemInstance newItem)
    {
        if (itemTemplate == null)
        {
            itemTemplate = newItem.itemTemplate;
            count = newItem.count;
            return 0;
        }
        if (CanStack(newItem))
        {
            if (count + newItem.count <= itemTemplate.MaxStackSize)
            {
                count += newItem.count;
                return 0;
            }
            else
            {
                int remaining = (count + newItem.count) - itemTemplate.MaxStackSize;
                count = itemTemplate.MaxStackSize;
                newItem.count = remaining;
                return remaining;
            }
        }
        return 0;
         
    }
}
