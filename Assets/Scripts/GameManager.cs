using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Specialized;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    public GameObject fastEnemy;
    public GameObject cloud;
    public GameObject coin;
    public GameObject powerUp;
    public GameObject heart;

    private int score;
    public Player playerScript;
    private bool isPlayerAlive;

    private float startDelay = 1.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerUpText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
  

    // Start is called before the first frame update
    void Start()
    {
        isPlayerAlive = true;
        Instantiate(player, transform.position, Quaternion.identity);
        StartCoroutine(CreateEnemy());
        StartCoroutine(CreateFastEnemy());
        StartCoroutine(CreateCoin());
        StartCoroutine(CreatePowerup());
        StartCoroutine(CreateHeart());
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: 3";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

   IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(startDelay);
        Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 4f));
        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateFastEnemy()
    {
        yield return new WaitForSeconds(startDelay * 2);
        Instantiate(fastEnemy, new Vector3(UnityEngine.Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 4f));
        StartCoroutine(CreateFastEnemy());
    }

    IEnumerator CreateCoin()
    {
        yield return new WaitForSeconds(startDelay * 2);
        Instantiate(coin, new Vector3(-10.75f, UnityEngine.Random.Range(-4.16f, 0f), 0f), Quaternion.identity);
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));
        StartCoroutine(CreateCoin());
    }

    IEnumerator CreatePowerup()
    {
        yield return new WaitForSeconds(startDelay * 4);
        Instantiate(powerUp, new Vector3(UnityEngine.Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 5f));
        StartCoroutine(CreatePowerup());
    }

    IEnumerator CreateHeart()
    {
        yield return new WaitForSeconds(startDelay * 10);
        Instantiate(heart, new Vector3(UnityEngine.Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
        StartCoroutine(CreateHeart());
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
        finalScoreText.text = "Final Score: " + score;
    }

    public void UpdateLivesText(string howManyLives)
    {
        livesText.text = howManyLives;
    }

    public void UpdatePowerUpText(string whichPowerUp)
    {
        powerUpText.text = whichPowerUp;
    }

    public void GameOver()
    {
        isPlayerAlive = false;
        StopAllCoroutines();
        livesText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            //Restart the game
            SceneManager.LoadScene("Game");
        }
    }
}
