using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private int lives;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        speed = 8.0f;
        lives = 3;
        transform.position = (new Vector3(0, -4, 4));
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Shooting();
    }
    void Moving()
    {
        //player WASD movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        //if x position is bigger than 16.5f or smaller than -16.5f the player is outside the screen
        if (transform.position.x >= 16.5f || transform.position.x <= -16.5f)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        } 
        //if y position is < -6.24 the player is outside the screen
        //if y position is > 0 the player is above the bottom half
        if (transform.position.y <= -6.24f || transform.position.y >= 0)
        {
            transform.Translate(new Vector3(0, verticalInput * -1, 0) * Time.deltaTime * speed);
        }
        //this reverses the speed the player is going, preventing them from moving any further up or down without the need for collision boxes
    }
    void Shooting()
    {
        //if SPACE key is pressed create a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //create a bullet
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }

    }
}
