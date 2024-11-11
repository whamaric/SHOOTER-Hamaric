using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float horizontalScreenLimit;

    public GameObject explosion;
    public GameObject bullet;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        horizontalScreenLimit = 11.5f;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        //teleports player from one side to the other
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //prevents player from going above y = 0 and below y = -4.16
        if (transform.position.y <= -4.16f || transform.position.y >= 0)
        {
            transform.Translate(new Vector3(0, verticalInput * -1, 0) * Time.deltaTime * speed);
            //this works by reversing the speed. I'm doing it this way because the default way made the player jittery when reaching the bounds, but this looks smoother.
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        lives--;
        if (lives == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
