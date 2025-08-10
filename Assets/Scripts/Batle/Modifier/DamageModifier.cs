using UnityEngine;

[CreateAssetMenu(fileName = "DamageModifier", menuName = "Scriptable Objects/DamageModifier")]
public abstract class DamageModifier : ScriptableObject
{
    public abstract float MultiplyDamade(float dmg);
    public abstract void ApplyEffect(LifeManager tagert);
}
