using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class DiagEnemy : MonoBehaviour
{
    public float movementInterval = 1f;
    // Start is called before the first frame update
    void Start()
    {
        movementInterval = Random.Range(-3f,3f);
    }

    // Update is called once per frame
    void Update()
    {
        DiagEnemyMovement();
    }

    void DiagEnemyMovement(){
        
        transform.Translate(new UnityEngine.Vector3(movementInterval, -1, 0) * Time.deltaTime * 5f);

        if (transform.position.x >= 14f || transform.position.x <= -14f)
        {
            movementInterval = movementInterval * -1;
        } 

        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
            movementInterval = Random.Range(-1f,1f);
        }
    }
}
