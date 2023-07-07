using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed
    {
        get => _bulletSpeed;
    }

	public GameObject _explosion;
    private float _bulletSpeed = 500.0f;
    private int _damage = 10;
	
	void OnCollisionEnter(Collision col)
    {
    	GameObject e = Instantiate(_explosion, this.transform.position, Quaternion.identity);

        Health health = col.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(_damage);
        }

    	Destroy(e,1.5f);
    	Destroy(this.gameObject);
    }
}
