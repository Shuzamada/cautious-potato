using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Turret Attack/Bullet")]
public class BulletAttack : TurretAttackBase
{
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private int burst = 300;
    [SerializeField] private float spread = 2f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private List<DamageModifier> damageModifiers = new List<DamageModifier>();
    public override void Shoot(Turret t)
    {
        for (int i = 0; i < burst; i++)
        {
            GameObject bullet = Object.Instantiate(t.BulletPrefab, t.Muzzle.position, t.Muzzle.rotation);
            DamageInfo damageInfo = new DamageInfo
            {
                baseDamage = damage,
                modifiers = new List<DamageModifier>(damageModifiers)
            };

            // Передаем в пулю модификатор
            bullet.GetComponent<Bullet>().Initialize(damageInfo);

            Vector3 dir = t.Muzzle.forward + Random.insideUnitSphere * spread * 0.01f;
            bullet.GetComponent<Rigidbody>().linearVelocity = dir.normalized * bulletSpeed;
            Destroy(bullet, 3);
        }
    }
}

