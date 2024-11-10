using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BasicEnemyMovement();
    }

    void BasicEnemyMovement(){
            transform.Translate(new UnityEngine.Vector3(0, -1, 0) * Time.deltaTime * 4f);
        
        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }
}
