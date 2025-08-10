using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Database/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemInterface> allItems = new List<ItemInterface>();
    [SerializeField] private string itemFolderPath = "Assets/Item";

    private Dictionary<int, ItemInterface> itemDictionary;

//#if UNITY_EDITOR
    [ContextMenu("Reload Items From Folder")]
    void LoadAllItemsFromFolder()
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:ItemInterface", new[] { itemFolderPath });
        
        allItems.Clear();
        foreach (string guid in guids)
        {
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            ItemInterface item = UnityEditor.AssetDatabase.LoadAssetAtPath<ItemInterface>(assetPath);
            
            if (item != null)
            {
                allItems.Add(item);
                Debug.Log($"Found {item.GetType().Name}: {item.Name} (ID: {item.Id})");
            }
        }
        
        // ��������� �� ID
        //allItems = allItems.OrderBy(item => item.Id).ToList();
        
        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"Loaded {allItems.Count} items from {itemFolderPath}");
    }
//#endif

    void OnEnable()
    {
        InitializeDictionary();
    }

    void InitializeDictionary()
    {
        itemDictionary = new Dictionary<int, ItemInterface>();
        foreach (var item in allItems)
        {
            if (item != null)
            {
                itemDictionary[item.Id] = item;
            }
        }
    }

    public ItemInterface GetItemById(int id)
    {
        return itemDictionary.TryGetValue(id, out var item) ? item : null;
    }

    public ItemInterface GetRandomItem()
    {
        if (allItems.Count == 0) return null;
        return allItems[UnityEngine.Random.Range(0, allItems.Count)];
    }

    public List<ItemInterface> GetAllItems()
    {
        return new List<ItemInterface>(allItems);
    }

    // ��������� ��������� �� ����
/*    public List<T> GetItemsByType<T>() where T : ItemInterface
    {
        return allItems.OfType<T>().ToList();
    }*/
}

