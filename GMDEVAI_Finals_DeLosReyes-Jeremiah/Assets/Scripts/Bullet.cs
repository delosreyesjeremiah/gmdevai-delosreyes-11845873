using System.Collections;
using UnityEngine;

public enum Owner
{
    Player,
    Elemental
}

public class Bullet : MonoBehaviour
{
    #region Properties

    public Owner Owner
    {
        get => _owner;
        set => _owner = value;
    }

    #endregion


    #region Fields

    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetimeDuration;

    private Owner _owner;

    #endregion

    #region Unity Messages

    private void Start()
    {
        StartCoroutine(CO_LifetimeDuration());
    }

    private void LateUpdate()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_owner == Owner.Player)
        {
            ElementalAI elementalAI = collision.gameObject.GetComponent<ElementalAI>();

            if (elementalAI != null)
            {
                elementalAI.Health.TakeDamage(_damage);
            }
        }
        else
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                player.Health.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }

    #endregion

    #region Private Methods

    private IEnumerator CO_LifetimeDuration()
    {
        yield return new WaitForSeconds(_lifetimeDuration);
        Destroy(gameObject);
    }

    #endregion
}
