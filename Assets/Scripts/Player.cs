using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;

    public int lives;
    private float speed;
    private int shooting;
    private bool shielded;
    private float horizontalScreenLimit;

    public GameObject bullet;
    public GameObject explosion;
    public GameObject thruster;
    public GameObject shield;

    public AudioClip coinPickup;
    public AudioClip powerUpPickup;
    public AudioClip powerDown;
    public AudioClip gainHeart;
    public AudioSource audioSource;
    public float volume = 1f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        speed = 6f;
        shooting = 1;
        shielded = false;
        horizontalScreenLimit = 11.5f;
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    Instantiate(bullet, transform.position + new Vector3(-0.1f, 1f, 0f), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bullet, transform.position + new Vector3(-0.6f, 1f, 0f), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.4f, 1f, 0f), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bullet, transform.position + new Vector3(-1.1f, 1f, 0f), Quaternion.Euler(0, 0, 25));
                    Instantiate(bullet, transform.position + new Vector3(-0.1f, 1f, 0f), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.9f, 1f, 0f), Quaternion.Euler(0, 0, -25));
                    break;
            }
        }
    }

    public void LoseLives()
    {
        if (shielded == true)
        {
            shielded = false;
            shield.gameObject.SetActive(false);
            audioSource.PlayOneShot(powerDown, volume);
            gameManager.UpdatePowerUpText("");
            StopAllCoroutines();
        }
        else if (shielded == false)
        {
            lives--;
            gameManager.UpdateLivesText("Lives: " + lives);
        }

        if (lives == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            gameManager.GameOver();
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(4f);
        speed = 6f;
        thruster.gameObject.SetActive(false);
        audioSource.PlayOneShot(powerDown, volume);
        gameManager.UpdatePowerUpText("");
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(4f);
        shooting = 1;
        audioSource.PlayOneShot(powerDown, volume);
        gameManager.UpdatePowerUpText("");
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(6f);
        shielded = false;
        shield.gameObject.SetActive(false);
        audioSource.PlayOneShot(powerDown, volume);
        gameManager.UpdatePowerUpText("");

    }

    public void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Coin")
        {
            gameManager.EarnScore(1);
            audioSource.PlayOneShot(coinPickup, volume);
            Destroy(whatDidIHit.gameObject);
        }
        else if (whatDidIHit.tag == "Heart")
        {
            audioSource.PlayOneShot(gainHeart, volume);
            lives++;
            gameManager.UpdateLivesText("Lives: " + lives);
            Destroy(whatDidIHit.gameObject);
        }
        else if (whatDidIHit.tag == "PowerUp")
        {
            audioSource.PlayOneShot(powerUpPickup, volume);
            int powerupType = UnityEngine.Random.Range(1, 5);
            switch (powerupType)
            {
                case 1:
                    //speed
                    speed = 9f;
                    gameManager.UpdatePowerUpText("Picked up Speed!");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //double shot
                    shooting = 2;
                    gameManager.UpdatePowerUpText("Picked up Double Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 3:
                    //triple shot
                    shooting = 3;
                    gameManager.UpdatePowerUpText("Picked up Triple Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    //shield
                    shielded = true;
                    shield.gameObject.SetActive(true);
                    gameManager.UpdatePowerUpText("Picked up Shield!");
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
            Destroy(whatDidIHit.gameObject);
        }
    }
}
