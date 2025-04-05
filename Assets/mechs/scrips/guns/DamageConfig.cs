using UnityEngine;


[CreateAssetMenu(fileName = "Damage Config", menuName = "guns/Damage Config", order = 0)]
public class DamageConfig : ScriptableObject
{
    public int damage;
    
    public int GetDamage()
    {
        return damage;
    }
}
