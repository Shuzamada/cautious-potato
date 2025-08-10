using UnityEngine;
using System;
using System.Collections.Generic;
public class Turret : PlaceableBase
{
    [SerializeField] private TurretAttackBase attackStrategy;
    [SerializeField] private Transform muzzle; // откуда выстрел
    [SerializeField] private Transform rotatingPart;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float detectionRadius = 15f;
    private List<Transform> enemiesInRange = new List<Transform>();
    private List<DamageModifier> modifiers = new List<DamageModifier>();


    public Transform Muzzle => muzzle;
    public GameObject BulletPrefab => bulletPrefab;
    public Transform Target { get; set; }

    float cooldown;


    void Start()
    {
        SphereCollider sc = gameObject.AddComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = detectionRadius;
    }
    void Update()
    {
        Target = FindClosestEnemy();
        if (Target == null) return;
        RotateTowardsTarget();

        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            attackStrategy.Shoot(this);
            cooldown = 1f / fireRate;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            //Debug.Log("кто-то в радиусе атаки турели");
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            enemiesInRange.Remove(other.transform);
        }

    }

    private Transform FindClosestEnemy()
    {
        Transform closest = null;
        float minSqrDist = Mathf.Infinity;
        Vector3 pos = transform.position;

        foreach (var enemy in enemiesInRange)
        {
            if (enemy == null) continue;
            float sqrDist = (enemy.position - pos).sqrMagnitude;
            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                closest = enemy;
            }
        }
        return closest;
    }
    private void RotateTowardsTarget()
    {
        if (Target == null) return;

        // Вектор направления до цели
        Vector3 direction = Target.position - rotatingPart.position;
        direction.y = 0f; 

        if (direction.sqrMagnitude < 0.01f) return;

        // Целевой угол
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Плавно поворачиваем (optional)
        float turnSpeed = 10f;            // градусы/сек
        rotatingPart.rotation = Quaternion.Lerp(
            rotatingPart.rotation,
            lookRotation,
            turnSpeed * Time.deltaTime);
    }
    public void AddModifier(DamageModifier modifier)
    {
        modifiers.Add(modifier);
        Debug.Log($"Турель {name} получила модификатор {modifier.name}");
    }
    
    public void RemoveModifier(DamageModifier modifier)
    {
        modifiers.Remove(modifier);
        Debug.Log($"Турель {name} потеряла модификатор {modifier.name}");
    }

}
