using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour 
{
    public Vector2 Life;
    public Tilemap Tilemap;
    public Slider ProgressBar;
    public Transform Player;
    public float ColorMax;
    public float LifeReduceRate;

    public float Score;
    private float TotalTime;
    private float OriginalHeight;

    public Text GameOverText;

    private void Awake()
    {
        OriginalHeight = Player.position.y;
    }

    void Update () 
    {
        var percent = Life.x / Life.y;
        var color = ColorMax * percent;
        Tilemap.color = new Color(color, color, color);

        Life.x -= LifeReduceRate * Time.deltaTime;

        Life = new Vector2(Mathf.Clamp(Life.x, 0, Life.y), Life.y); // Life can't go negative.

        ProgressBar.maxValue = Life.y;
        ProgressBar.minValue = 0;
        ProgressBar.value = Life.x;

        TotalTime += Time.deltaTime;
        Score = TotalTime + (Player.position.y - OriginalHeight);

        if (Life.x == 0)
        {
            GameOverText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    public void AnimalCollected()
    {
        Life.x += 20;
        Life = new Vector2(Mathf.Clamp(Life.x, 0, Life.y), Life.y); // Life can't go negative.
        OriginalHeight -= 1.5f;
    }
}