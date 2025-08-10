using UnityEngine;
using System;
using System.Collections.Generic;
public class DamageInfo
{
    public List<DamageModifier> modifiers = new List<DamageModifier>();
    public float baseDamage;

    public float CalcFinalDamage(LifeManager target)
    {
        float finalDamage = baseDamage;
        foreach (DamageModifier modifier in modifiers)
        {
            finalDamage = modifier.MultiplyDamade(finalDamage);
        }
        return finalDamage;
    }

    public void ApplyAllEffects(LifeManager target)
    {
        foreach (var modifier in modifiers)
        {
            modifier.ApplyEffect(target);
        }
    }
}
