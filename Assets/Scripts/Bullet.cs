using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 10f);
        //when the bullet is at y 10 it is off screen
        if (transform.position.y > 10f)
        {
            Destroy(this.gameObject);
        }
    }
// this is meant to destroy the bullet and whatever the bullet collides with but it is not working
    //void OnTriggerEnter(Collider col){
    //    Destroy(col.gameObject);
    //}

   // void OnTriggerEnter(){
    //    Destroy(gameObject);
   // }
}
