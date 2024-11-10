using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject diagEnemy;
    private float startDelay = 1.0f;
    float spawnInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //this spawns the player
        Instantiate(player, transform.position, Quaternion.identity);
        //this repeats the enemy creation and how often it happens
        Invoke(nameof(CreateBasicEnemy), startDelay);
        Invoke(nameof(CreateDiagonalEnemy), startDelay *2);
    }

    // Update is called once per frame
    void Update()
    {

    }
// this is for spawning enemies
    void CreateBasicEnemy()
    {
        //introduces actual random intervals
        spawnInterval = Random.Range(1f, 4f);

        //this spawns a basic enemy
        Instantiate(enemy, new Vector3(Random.Range(-14.5f, 14.5f), 10f, 4f), Quaternion.identity);
        //instead of InvokeRepeating it will just be performed recursively here
        Invoke(nameof(CreateBasicEnemy), spawnInterval);
    }

    void CreateDiagonalEnemy(){
        spawnInterval = Random.Range(3f, 7f);
        
        Instantiate(diagEnemy, new Vector3(Random.Range(-14.5f, 14.5f), 10f, 4f), Quaternion.identity);
        
        Invoke(nameof(CreateDiagonalEnemy), spawnInterval);
    }

}

