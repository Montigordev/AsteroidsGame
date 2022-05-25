using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : Bullet
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("ProjectileTag"))
        {
            other.gameObject.GetComponent<IDamageable>().DestroyEntity();
        }
    }

}
