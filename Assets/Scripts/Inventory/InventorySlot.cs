using UnityEngine;

public class InventorySlot
{
    public ItemInstance item;

    public bool isEmpty()
    {
        return item == null;
    }
    public void AddItem(ref ItemInstance newItem)
    {
        if (item == null)
        {
            item = newItem;
        }
        else if (item.CanStack(newItem) && item.count < item.itemTemplate.MaxStackSize)
        {
             item.AddItem(ref newItem);
        }
    }
}
