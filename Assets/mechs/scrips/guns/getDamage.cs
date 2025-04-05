using UnityEngine;

public interface getDamage
{
    public int CurrentHealth { get; }
    public int MaxHealth { get; }

    public delegate void TakeDamageEvent(int damage);
    public event TakeDamageEvent OnTakeDamage;

    public delegate void DieEvent(Vector3 position);
    public event DieEvent OnDie;

    public void TakeDamage(int damage);
}
