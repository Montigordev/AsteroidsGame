using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    private Vector3 boundsSize;
    [SerializeField]
    private GameObject asteroidPrefab;
    [SerializeField]
    private GameObject ufoPrefab;

    [SerializeField]
    private ScreenBounds screenBounds;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float ufoSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroidEnum());
        StartCoroutine(SpawnUfoEnum());
    }

    private IEnumerator SpawnAsteroidEnum()
    {
        CalculateSpawnPosition();
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnAsteroidEnum());
    }
    private IEnumerator SpawnUfoEnum()
    {
        yield return new WaitForSeconds(ufoSpawnTime);
        CalculateUfoSpawnPosition();
        yield return new WaitForSeconds(ufoSpawnTime);
        StartCoroutine(SpawnUfoEnum());
    }

    private void CalculateSpawnPosition()
    {
        int asteroidAmount = Random.Range(1, 4);
        Vector2 spawnPos = Vector2.zero;
        for (int i = 0; i < asteroidAmount; i++)
        {
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0:
                    //North
                    spawnPos = new Vector2(Random.Range(-boundsSize.x/2,boundsSize.x/2), Random.Range(boundsSize.y/2 + 2f, boundsSize.y/2 + 3f));
                    break;
                case 1:
                    //South
                    spawnPos = new Vector2(Random.Range(-boundsSize.x/2, boundsSize.x/2), Random.Range(-boundsSize.y/2 - 1f, -boundsSize.y / 2 - 3f));
                    break;
                case 2:
                    //West
                    spawnPos = new Vector2(Random.Range(-boundsSize.x / 2 - 2f, -boundsSize.x / 2 - 3f), Random.Range(-boundsSize.y/2, boundsSize.y/2));
                    break;
                case 3:
                    //East
                    spawnPos = new Vector2(Random.Range(boundsSize.x/2 + 2f, boundsSize.x/2 + 3f), Random.Range(-boundsSize.y/2, boundsSize.y/2));
                    break;
                default:
                    Debug.Log("No side");
                    break;
            }
            SpawnAsteroid(spawnPos);
        }
    }

    private void SpawnAsteroid(Vector2 _spawnPos)
    {
        GameObject asteroid = Instantiate(asteroidPrefab, _spawnPos, Quaternion.identity);

        Asteroid a = asteroid.GetComponent<Asteroid>();
        a.Direction = new Vector2(Random.Range(-boundsSize.x / _spawnPos.x, boundsSize.x / _spawnPos.x),
                    Random.Range(-boundsSize.y / _spawnPos.y, boundsSize.y / _spawnPos.y)) - _spawnPos;
        a.ScreenBounds = screenBounds;
    }

    private void SpawnUfo(Vector2 _spawnPos, Vector2 _direction)
    {
        GameObject ufo = Instantiate(ufoPrefab, _spawnPos, Quaternion.identity);
        UfoScript u = ufo.GetComponent<UfoScript>();
        u.Direction = _direction;
    }

    private void CalculateUfoSpawnPosition()
    {
        Vector2 spawnPos = Vector2.zero;
        Vector2 spawnDir = Vector2.zero;
        int side = Random.Range(0,2);
        switch (side)
        {
            case 0:
                //West
                spawnPos = new Vector2(Random.Range(-boundsSize.x / 2 - 0.5f, -boundsSize.x / 2 - 1f), Random.Range(-boundsSize.y / 2, boundsSize.y / 4));
                spawnDir = Vector2.right;
                break;
            case 1:
                //East
                spawnPos = new Vector2(Random.Range(boundsSize.x / 2 + 0.5f, boundsSize.x / 2 + 1f), Random.Range(-boundsSize.y / 2, boundsSize.y / 4));
                spawnDir = Vector2.left;
                break;
            default:
                //no side
                break;
        }
        SpawnUfo(spawnPos, spawnDir);
    }

    public void SetBoundsSize(Vector3 _size)
    {
        boundsSize = _size;
    }
}
