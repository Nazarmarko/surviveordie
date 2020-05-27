using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Range(0,20)]
    public float speed, stopDistance, retreatDistance;

    private Transform target;

    private SpriteRenderer mySpriteRenderer;

    public int health;
    [SerializeField]
    private int EnemyDamage;
    // private Animator EnemyAnimator;
    public GameObject bloodEffect;

    public bool isShooting;
    private void OnEnable()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //EnemyAnimator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
   void Update()
    {

        #region Movement
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        print(distanceToTarget);
        if (distanceToTarget > stopDistance) 
        {
            //EnemyAnimator.SetBool("Attack", false);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
       else if (distanceToTarget < stopDistance && distanceToTarget > retreatDistance)
        {
            transform.position = this.transform.position;

          /*  Attack(EnemyDamage);
             EnemyAnimator.SetBool("Attack", true);*/
        }
        else if (distanceToTarget < retreatDistance)
        {
            print("fuckMeWell");
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
        #endregion

        #region SpriteFlip
        if (target.position.x < this.transform.position.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else 
        {
            mySpriteRenderer.flipX = false;
        }
        #endregion

        #region HealthTracker
        if (health <= 0)
        {
               Death();
                Destroy(gameObject, 0.5f);          
        }
        #endregion
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void Death()
    {
        //  EnemyAnimator.SetTrigger("Death");
        //     Player.Instance.GoBattle();
        /*if (Player.Instance.quest.IsActive) 
        {
            Player.Instance.quest.goal.EnemyKilled();
            if (Player.Instance.quest.goal.IsReached())
            {
                //  experience += quest.experienceReward;
                //  gold += quest.goldReward;
                Player.Instance.quest.Complete();
            }
        }    
        */
    }
    void Attack(float damageToGive) 
    {
        if (isShooting)
        {
            Debug.Log("piyyy" + damageToGive);
        }
        else if(!isShooting)
        {
            print("xaaiiaaaa" +  damageToGive);
        }
    }

  
}
