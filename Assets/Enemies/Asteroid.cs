using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : Enemy
{
    [SerializeField]
    private GameObject shard;

    public override void DestroyEntity()
    {
        //Spawn asteroid shards 
        if (shard != null)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject asteroid = Instantiate(shard, transform.position, Quaternion.identity);
                Asteroid a = asteroid.GetComponent<Asteroid>();
                a.Direction = Random.insideUnitCircle.normalized;
                a.ScreenBounds = ScreenBounds;
            }
        }
        base.DestroyEntity();
    }
}
