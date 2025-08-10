using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged,
    Magic
}


[CreateAssetMenu (fileName = "NewWeapon", menuName = "Item/Weapon")]
public class WeaponItem : ItemInterface
{
    [Header("Weapon Properties")]
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private WeaponType weaponType;

/*    public override void Use(Player player)
    {
        Debug.Log($"Using weapon: {Name} with damage: {damage}");
    }*/
}
