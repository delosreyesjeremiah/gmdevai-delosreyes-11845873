using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Properties

    public float MovementSpeed
    {
        get => _movementSpeed;
    }

    public float RotationSpeed
    {
        get => _rotationSpeed;
    }

    public float FireRate
    {
        get => _fireRate;
    }

    public Health Health
    {
        get => _health;
    }

    public Gun Gun
    {
        get => _gun;
    }

    #endregion

    #region Fields

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _fireRate;

    [SerializeField] private Slider _healthBarSlider;

    private Health _health;
    private Gun _gun;

    #endregion

    #region Unity Messages

    private void Awake()
    {
        _health = GetComponent<Health>();
        _gun = GetComponentInChildren<Gun>();
    }

    private void Start()
    {
        _gun.Owner = Owner.Player;

        _healthBarSlider.value = 1.0f;
    }

    private void OnEnable()
    {
        _health.OnCurrentHealthChanged += UpdateHealthBarSlider;
        _health.OnDeath += Death;
    }

    private void OnDisable()
    {
        _health.OnCurrentHealthChanged -= UpdateHealthBarSlider;
        _health.OnDeath -= Death;
    }

    #endregion

    #region Private Methods

    private void UpdateHealthBarSlider()
    {
        _healthBarSlider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
    }

    private void Death()
    {
        GameManager.Instance.GameOver();
    }

    #endregion
}
