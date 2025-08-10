using UnityEngine;

public class PlaceableBase : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private int maxHP = 100;
    protected int hp;

    void Avake()
    {
        hp = maxHP;
    }

    protected virtual void OnPlace()
    {

    }

    private void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
