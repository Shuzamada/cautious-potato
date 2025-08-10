using UnityEngine;
using System.Collections;
public class LifeManager : MonoBehaviour
{
    [SerializeField] private float curHP;
    [SerializeField] private float maxHP = 100;
    [SerializeField] private float lowHpResist = 0.5f;
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float dmg)
    {
        curHP -= dmg;
    }

    public void ApplyBurnEffect(int duration, float freq)
    {
        //вызов корутины надо бы сделать 
    }
    private IEnumerator BurnCoroutine(int duration, float freq)
    {
         yield return new WaitForSeconds(1f);
    } //а саму корутину доделать бы
}
