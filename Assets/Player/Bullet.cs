using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameEntity
{
    [SerializeField]
    protected float bulletLifeTime = 4f;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(DeathEnum(bulletLifeTime));
    }

    protected IEnumerator DeathEnum(float _lifetime)
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("ProjectileTag"))
        {
            other.gameObject.GetComponent<IDamageable>().DestroyEntity();
            Destroy(gameObject);
        }    
    }
}
