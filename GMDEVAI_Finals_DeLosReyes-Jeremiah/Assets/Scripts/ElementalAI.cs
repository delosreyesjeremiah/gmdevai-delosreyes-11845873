using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This enum represents the possible states of the Elemental AI
public enum ElementalAIState
{
    Flock, // State for flocking behavior
    Chase, // State for chasing a target
    Attack // State for attacking a target
}

public class ElementalAI : MonoBehaviour
{
    #region Properties

    public ElementalAIState ElementalAIState
    {
        get => _elementalAIState;
        set => _elementalAIState = value;
    }

    public Health Health
    {
        get => _health;
    }

    public Transform Target
    {
        get => _target;
    }

    public bool IsShooting
    {
        get => _isShooting;
    }

    #endregion

    #region Fields

    [SerializeField] private ElementalRoom _ownElementalRoom; // Reference to the Elemental Room that owns this Elemental AI
    [SerializeField] private List<ElementalRoom> _otherElementalRooms; // List of other ElementalRooms that influence this Elemental AI

    [SerializeField] private ElementalAIState _elementalAIState; // Current state of the Elemental AI
    [SerializeField] private float _fireRate; // Rate at which the Elemental AI shoots

    [SerializeField] private Slider _healthBarSlider; // Reference to the health bar slider for UI display

    private Health _health; // The Health component of the Elemental AI
    private Gun _gun; // The Gun component used by the Elemental AI

    private Transform _target; // The current target that the Elemental AI is interacting with

    private bool _isShooting = false; // Flag indicating whether the Elemental AI is shooting

    #endregion

    #region Unity Messages

    private void Awake()
    {
        _health = GetComponent<Health>();
        _gun  = GetComponentInChildren<Gun>();
    }

    private void Start()
    {
        _gun.Owner = Owner.Elemental;
        _healthBarSlider.value = 1.0f;

        GameManager.Instance.AddElemental();
    }

    private void OnEnable()
    {
        // Subscribing to events and setting up initial behaviors
        _health.OnCurrentHealthChanged += UpdateHealthBarSlider;
        _health.OnDeath += Death;
        _ownElementalRoom.OnTargetEntered += ChaseTarget;

        foreach (ElementalRoom elementalRoom in _otherElementalRooms)
        {
            elementalRoom.OnTargetExited += ChaseTarget;
        }
    }

    private void OnDisable()
    {
        // Unsubscribing from events and cleaning up behaviors
        _health.OnCurrentHealthChanged -= UpdateHealthBarSlider;
        _health.OnDeath -= Death;
        _ownElementalRoom.OnTargetEntered -= ChaseTarget;

        foreach (ElementalRoom elementalRoom in _otherElementalRooms)
        {
            elementalRoom.OnTargetExited -= ChaseTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null && _target != null)
        {
            _elementalAIState = ElementalAIState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null && _target != null)
        {
            _isShooting = false;
            StopShooting();
            _elementalAIState = ElementalAIState.Chase;
        }
    }

    #endregion

    #region Public Methods

    public void StartShooting()
    {
        StartCoroutine(CO_Shoot());
    }

    #endregion

    #region Private Methods

    private IEnumerator CO_Shoot()
    {
        _isShooting = true;

        while (_isShooting)
        {
            _gun.Shoot();
            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void StopShooting()
    {
        StopCoroutine(CO_Shoot());
    }

    private void ChaseTarget()
    {
        if (_ownElementalRoom.Target != null)
        {
            _target = _ownElementalRoom.Target;
            _elementalAIState = ElementalAIState.Chase;
        }  
    }

    private void UpdateHealthBarSlider()
    {
        _healthBarSlider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
    }

    private void Death()
    {
        GameManager.Instance.RemoveElemental();
        Destroy(gameObject);
    }

    #endregion
}
