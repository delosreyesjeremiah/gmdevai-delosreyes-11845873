using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _turret;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(_bullet, _turret.transform.position, _turret.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(_turret.transform.forward * bullet.GetComponent<Bullet>().BulletSpeed);
    }
}
