using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameEntity, IDamageable 
{

    [SerializeField]
    protected int score;

    public override void DestroyEntity()
    {
        GameObject.FindGameObjectWithTag("GameManagerTag").GetComponent<UiManager>().AddScore(score);
        Destroy(gameObject);
    }

}
