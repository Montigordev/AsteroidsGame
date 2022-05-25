using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoScript : Enemy
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float shootDelay;

    private GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootPlayerEnum());
    }

    protected override void Update()
    {
        if (player != null)
        {
            Direction = player.transform.position - transform.position;
        }
        base.Update();
    }

    private IEnumerator ShootPlayerEnum()
    {
        yield return new WaitForSeconds(shootDelay);
        ShootPlayer(bullet);
        if (player != null)
        {
            StartCoroutine(ShootPlayerEnum());
        }
    }

    private void ShootPlayer(GameObject _projectile)
    {
        GameObject b = Instantiate(_projectile, transform.position, Quaternion.identity);
        b.gameObject.GetComponent<Bullet>().Direction = Direction;
        b.gameObject.transform.rotation = transform.rotation;
    }

}
