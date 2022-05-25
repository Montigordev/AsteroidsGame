using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    private bool canShootBullet = true;
    private bool canShootLaser = true;
    [SerializeField]
    private float bulletDelay = 1f;
    [SerializeField]
    private float laserDelay = 1f;
    private float laserDelaytemp;
    public float LaserDelay
    {
        get
        {
            return laserDelay;
        }
        set
        {
            laserDelay = value;
        }
    }
    [SerializeField] 
    private float laserRefillTime = 5f;
    [SerializeField]
    private int laserAmount;
    public int LaserAmount
    {
        get
        {
            return laserAmount;
        }
        set
        {
            laserAmount = value;
        }
    }

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject laser;

    public void ShootBullet(InputAction.CallbackContext context)
    {
        if (context.performed && canShootBullet)
        {
            Shoot(bullet, ShootBulletDelay());
        }
    }
    private IEnumerator ShootBulletDelay()
    {
        canShootBullet = false;
        yield return new WaitForSeconds(bulletDelay);
        canShootBullet = true;
    }

    public void ShootLaser(InputAction.CallbackContext context)
    {
        if (context.performed && canShootLaser && laserAmount > 0)
        {
            Shoot(laser, ShootLaserDelay());
        }
    }

    private IEnumerator ShootLaserDelay()
    {
        canShootLaser = false;
        laserAmount -= 1;
        laserDelaytemp = laserDelay;
        StartCoroutine(AmmoUpEnum());
        for (float i = laserDelay; i > 0; i -= Time.deltaTime)
        {
            laserDelay = i;
            yield return null;
        }
        laserDelay = laserDelaytemp;
        canShootLaser = true;
    }

    private IEnumerator AmmoUpEnum()
    {
        yield return new WaitForSeconds(laserRefillTime);
        laserAmount += 1;
    }

    private void Shoot(GameObject _projectile, IEnumerator _Delay)
    {
        GameObject b = Instantiate(_projectile, transform.position, Quaternion.identity);
        b.gameObject.GetComponent<Bullet>().Direction = Vector2.up;
        b.gameObject.transform.rotation = transform.rotation;
        StartCoroutine(_Delay);
    }
}
