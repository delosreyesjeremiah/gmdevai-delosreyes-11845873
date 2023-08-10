using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Properties

    public Owner Owner
    {
        get => _owner;
        set => _owner = value;
    }

    #endregion

    #region Fields

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzle;

    private Owner _owner;

    #endregion

    #region Public Methods

    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Owner = _owner;
    }

    #endregion
}
