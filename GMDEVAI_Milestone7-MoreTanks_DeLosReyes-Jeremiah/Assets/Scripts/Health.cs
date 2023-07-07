using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public event Action<Health> OnCurrentHealthChanged;

    public int CurrentHealth
    {
        get => _currentHealth;
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        _healthSlider.value = (float)_currentHealth/(float)_maxHealth;
        OnCurrentHealthChanged?.Invoke(this);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
