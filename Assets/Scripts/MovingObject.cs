using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public int myType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myType == 1)
        {
            //I am a bullet
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 10f);
        }
        else if (myType == 2)
        {
            //I am an enemy
            transform.Translate(new Vector3(0, UnityEngine.Random.Range(-1f, -0.5f), 0) * Time.deltaTime * 3f);
        }
        else if (myType == 3)
        {
            //I am a cloud
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Random.Range(2f, 6f));
        }
        else if (myType == 4)
        {
            //I am a fast enemy
            transform.rotation = new UnityEngine.Quaternion(0f, 0f, 180f, 0f);
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 7f);
        }
        else if (myType == 5)
        {
            //I am a coin
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 4f);
        }

        if ((transform.position.x > 10.5f || transform.position.y > 9f ||  transform.position.y <= -9f) && myType != 3)
        //destroys objects out of bounds, including coins going to the right
        {
            Destroy(this.gameObject);
        } else if (transform.position.y <= -9f && myType == 3)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 9f, 0);
        }
    }
}
