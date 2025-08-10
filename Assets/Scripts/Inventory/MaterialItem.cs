using UnityEngine;

[CreateAssetMenu(fileName = "MaterialItem", menuName = "Item/MaterialItem")]
public class MaterialItem : ItemInterface
{
    [Header("Material Properties")]
    [SerializeField] private float weight;
/*    public override void Use(Player player)
    {
        Debug.Log($"Using Materials: {Name}");
    }*/
}