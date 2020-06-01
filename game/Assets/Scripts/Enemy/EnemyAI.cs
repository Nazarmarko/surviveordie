using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Singleton<EnemyAI>
{
    [Range(.1f,30)]
   public float enemyMoveSpeed, minMoveDistance, retreatDistance, startShootTime, maxHealth;

    private float currentHealth;

    public GameObject bullet;
    public Transform[] moveSpotsTransform;
    public Transform playerTransform;
    private Animator enemyAnim;

    void Awake()
    {
        currentHealth = maxHealth;
        enemyAnim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
     void Update()
    {
       float distancetoTarget = Vector2.Distance(transform.position, playerTransform.position);

        if (distancetoTarget < minMoveDistance && distancetoTarget > retreatDistance)
        {
            enemyAnim.SetBool("isFollowing", true);
            enemyAnim.SetBool("isPatrolling", false);
        }
        else if(distancetoTarget < minMoveDistance && distancetoTarget < retreatDistance)
        {
            enemyAnim.SetBool("isFollowing", false);
            enemyAnim.SetBool("isPatrolling", false);
        }
        else if (distancetoTarget >= minMoveDistance && retreatDistance < distancetoTarget)
        {
            enemyAnim.SetBool("isFollowing", false);
            enemyAnim.SetBool("isPatrolling", true);
        }

        if(currentHealth <=0)
        {
            Debug.Log("FuckinDie");
        }
    }
    public void TakeDamage(float damageFromPlayer) { currentHealth -= damageFromPlayer; Debug.Log("FuckinDie"); }
}
