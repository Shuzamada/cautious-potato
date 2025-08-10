using UnityEngine;

public class FireModifier : DamageModifier
{
    [SerializeField] public float dmgMultyplier = 0.9f;

    [SerializeField] public int duration = 5;
    [SerializeField] public float freq = 0.5f;
    [SerializeField] public float dmg = 3f;

    public override float MultiplyDamade(float dmg)
    {
        return dmg * dmgMultyplier;
    }
    public override void ApplyEffect(LifeManager target)
    {
        target.ApplyBurnEffect(duration, freq);
    }
}
