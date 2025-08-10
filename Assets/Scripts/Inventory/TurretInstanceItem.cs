using UnityEngine;

[System.Serializable]
public class TurretItemInstance : ItemInstance
{

    public TurretItemInstance(TurretItem template, int count = 1)
        : base(template, count) { }

    public override void UseRMB() //потом надо сделать универсальную логику строительства
    {
        Debug.Log("turret use rmb");
        var turretItem = itemTemplate as TurretItem;

        if (turretItem == null) return;
        Transform cam = Camera.main.transform;
        Vector3 pos = cam.position + cam.forward * 2f;

        Object.Instantiate(turretItem.TurretPrefab, pos, Quaternion.identity);
    }
    public override void UseLMB()
    {
        Debug.Log("turret use lmb");
    }
}