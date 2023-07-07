using UnityEngine;

public class TankAI : MonoBehaviour
{
    public GameObject Player
    {
        get => _player;
    }

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _turret;

    private Animator _animator;
    private Health _health;
    private float _fireRate = 0.5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();

        _animator.SetInteger("health", _health.MaxHealth);
        _health.OnCurrentHealthChanged += UpdateCurrentHealth;
        
    }

    private void Update()
    {
        _animator.SetFloat("distance", Vector3.Distance(transform.position, _player.transform.position));
        
    }

    private void OnDestroy()
    {
        _health.OnCurrentHealthChanged -= UpdateCurrentHealth;
    }

    public void StartFiring()
    {
        InvokeRepeating(nameof(Fire), _fireRate, _fireRate);
    }

    public void StopFiring()
    {
        CancelInvoke(nameof(Fire));
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(_bullet, _turret.transform.position, _turret.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(_turret.transform.forward * bullet.GetComponent<Bullet>().BulletSpeed);
    }

    private void UpdateCurrentHealth(Health health)
    {
        _animator.SetInteger("health", health.CurrentHealth);
    }
}
