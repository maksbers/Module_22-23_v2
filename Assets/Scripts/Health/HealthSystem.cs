using UnityEngine;

public class HealthSystem
{
    private float _maxHealth;
    private float _currentHealth;

    public HealthSystem(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public bool IsDead => _currentHealth <= 0f;


    public float TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < 0f)
            _currentHealth = 0f;

        return _currentHealth;
    }
}
