using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMove;
    private PlayerShoot playerShoot;

    [SerializeField]
    private TextMeshProUGUI coordsText;
    private Vector2 coords;

    [SerializeField]
    private TextMeshProUGUI angleText;
    private float angle;

    [SerializeField]
    private TextMeshProUGUI speedText;
    private float speed;


    [SerializeField]
    private TextMeshProUGUI ammoText;
    private int ammo;

    [SerializeField]
    private TextMeshProUGUI coolDownText;
    private float coolDown;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;
    private int score;

    [SerializeField]
    private Button playButton;

    public Button PlayButton
    {
        get
        {
            return playButton;
        }
        set
        {
            playButton = value;
        }
    }
    [SerializeField]
    private List<TextMeshProUGUI> Texts;

    private void Start()
    {      
        player = GetComponent<GameMangerScript>().player;
        playerMove = player.GetComponent<PlayerMovement>();
        playerShoot = player.GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            CalculatePlayerStats();
        }
    }

    void CalculatePlayerStats()
    {
        coords = player.transform.position;
        angle = Mathf.Round(player.transform.rotation.eulerAngles.z);
        speed = Mathf.Round(playerMove.Speed);
        ammo = playerShoot.LaserAmount;
        coolDown = Mathf.Round(playerShoot.LaserDelay * 10f)/10f;
        DisplayText();
        DisplayScore();
    }

    void DisplayText()
    {
        speedText.text = "Speed: " + speed.ToString();
        ammoText.text = "Ammo: " + ammo.ToString();
        if (playerShoot.LaserDelay >= 3)
        {
            coolDownText.text = "Laser Ready";
        }
        else
        {
            coolDownText.text = "Cooldown: " + coolDown.ToString();
        }
        coordsText.text = "Coords: " + coords.ToString();
        angleText.text = "Angle: " + angle.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    void DisplayScore()
    {
        gameOverScoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
    }
    public void DisableText()
    {
        foreach (TextMeshProUGUI t in Texts)
        {
            if(t.isActiveAndEnabled)
            {
                t.gameObject.SetActive(false);
            }
            else
            {
                t.gameObject.SetActive(true);
            }
        }
    }
}
