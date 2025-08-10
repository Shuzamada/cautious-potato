using UnityEngine;

public class Bullet : MonoBehaviour
{
    private DamageInfo damageInfo;
    private bool debugMode = true;

    public void Initialize(DamageInfo info)
    {
        damageInfo = info;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Turret"))
            return;

        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            LifeManager lifeManager = collision.GetComponent<LifeManager>();
            Debug.Log("пуля попала");
            if (lifeManager != null && damageInfo != null)
            {
                float finalDamage = damageInfo.CalcFinalDamage(lifeManager);
                lifeManager.TakeDamage(finalDamage);
                damageInfo.ApplyAllEffects(lifeManager);

                if (debugMode)
                {
                    Debug.Log($"Нанесен урон {finalDamage} цели {collision.name}");
                }
            }
            
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
