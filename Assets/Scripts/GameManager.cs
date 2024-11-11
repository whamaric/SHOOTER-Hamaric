using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Specialized;
using System;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject fastEnemy;
    public GameObject cloud;
    public GameObject coin;
    private int score;
    private int lives;
    private float startDelay = 1.0f;
    float spawnIntervalEnemy = 0.5f;
    float spawnIntervalFast = 0.5f;
    float spawnIntervalCoin = 0.5f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        Invoke(nameof(CreateEnemy), startDelay);
        Invoke(nameof(CreateFastEnemy), startDelay * 2);
        Invoke(nameof(CreateCoin), startDelay * 2);
        CreateSky();
        score = 0;
        lives = 3;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemy()
    {
        spawnIntervalEnemy = UnityEngine.Random.Range(1f, 4f);
        Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        Invoke(nameof(CreateEnemy), spawnIntervalEnemy);
    }

    void CreateFastEnemy()
    {
        spawnIntervalFast = UnityEngine.Random.Range(3f, 6f);
        Instantiate(fastEnemy, new Vector3(UnityEngine.Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        //UnityEngine.Debug.Log("Fast Enemy Spawned");
        Invoke(nameof(CreateFastEnemy), spawnIntervalFast);
    }

    void CreateCoin()
    {
        spawnIntervalCoin = UnityEngine.Random.Range(5f, 8f);
        Instantiate(coin, new Vector3(-9.5f, UnityEngine.Random.Range(-4.16f, 0f), 0f), Quaternion.identity);
        Invoke(nameof(CreateCoin), spawnIntervalCoin);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int howMuch)
    {
        score = score + howMuch;
        scoreText.text = "Score: " + score;
    }

    public void LoseLives(int howMuch)
    {
        lives = lives - howMuch;
        livesText.text = "Lives: " + lives;

    }
}
