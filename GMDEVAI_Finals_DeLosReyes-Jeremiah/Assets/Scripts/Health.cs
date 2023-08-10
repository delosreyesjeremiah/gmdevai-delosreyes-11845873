using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth
    {
        get => _maxHealth;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
    }

    #region Events

    public Action OnCurrentHealthChanged;
    public Action OnDeath;

    #endregion

    #region Fields

    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    #endregion

    #region Unity Messages

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    #endregion

    #region Public Methods

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        OnCurrentHealthChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    #endregion
}
