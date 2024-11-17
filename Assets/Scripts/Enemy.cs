using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosion;
    public GameManager gameManager;
    public Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            playerScript.LoseLives();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } else if (whatDidIHit.tag == "Weapon")
        {
            gameManager.EarnScore(5);
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
