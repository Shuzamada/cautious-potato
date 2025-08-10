using UnityEngine;

[CreateAssetMenu(fileName = "TurretItem", menuName = "Item/Turret")]
public class TurretItem : ItemInterface
{
    [Header("Placed object")]
    [SerializeField] private GameObject turretPrefab;   // prefab с компонентом Turret

    public GameObject TurretPrefab => turretPrefab;
}
