using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Singleton<EnemyAI>
{
    [Range(.1f,30)]
   public float enemyMoveSpeed, minMoveDistance, minRetreatDistance;

    public Transform[] moveSpotsTransform;

    public Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
     void Update()
    {
       float distancetoTarget = Vector2.Distance(transform.position, playerTransform.position);

        if(distancetoTarget > minMoveDistance)
        {
            //move
        }
        else if(distancetoTarget < minMoveDistance && distancetoTarget <= minMoveDistance){
            //retreat
        }
        else
        {
            //shoot
        }
    }
}
