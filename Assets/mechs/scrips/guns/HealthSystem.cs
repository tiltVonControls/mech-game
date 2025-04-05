using UnityEngine;

public class HealthSystem : MonoBehaviour, getDamage
{
    [SerializeField]
    private int _MaxHealth = 100;
    [SerializeField]
    private int _Health = 100;

    public int CurrentHealth { get => _Health; private set => _Health = value; }
    public int MaxHealth { get => _MaxHealth; private set => _MaxHealth = value;}

    public event getDamage.TakeDamageEvent OnTakeDamage;
    public event getDamage.DieEvent OnDie;

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Clamp(damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if (damageTaken != 0)
        {
            OnDie?.Invoke(transform.position);
        }
    }
}
