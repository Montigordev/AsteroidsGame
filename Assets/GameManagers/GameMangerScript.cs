using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMangerScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    [SerializeField]
    private AsteroidSpawner asteroidSpawner;
    [SerializeField]
    private UiManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        asteroidSpawner = GetComponent<AsteroidSpawner>();
        uiManager = GetComponent<UiManager>();
    }

    public void GameOver()
    {
        asteroidSpawner.StopAllCoroutines();
        asteroidSpawner.enabled = false;
        uiManager.PlayButton.gameObject.SetActive(true);
        uiManager.DisableText();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
