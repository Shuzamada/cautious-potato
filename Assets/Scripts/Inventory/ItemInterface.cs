using UnityEngine;


public enum ItemType
{
    Material,
    Placeable,
    Weapon,
    Tools,
    Armor,
    Consumable,
    Quest
}


public abstract class ItemInterface : ScriptableObject
{
    [Header("Base Properties")]
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private ItemType type;
    [SerializeField] private bool isStackable;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int maxStackSize = 64;


   // public abstract void Use(Player player);

    // ���� �������
    public int Id => id;
    public string Name => name;
    public string Description => description;
    public ItemType Type => type;
    public bool IsStackable => isStackable;
    public Sprite Icon => icon;
    public GameObject ItemPrefab => itemPrefab;
    public int MaxStackSize => maxStackSize;
}
