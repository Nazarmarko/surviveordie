using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;

    public float offset = 1.6f;

    private Transform target;

    private SpriteRenderer mySpriteRenderer;

    public int health;
    [SerializeField]
    private int dmg;
    private Animator anim1;
    public GameObject bloodEffect;
    bool dead;

    



    private void Awake()
    {
        dead = false;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim1 = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > offset) 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        if (target.position.x < this.transform.position.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else 
        {
            mySpriteRenderer.flipX = false;
        }
        if (Vector2.Distance(transform.position, target.position) <= offset)
        {
            anim1.SetBool("Attack", true);
        }
        else
        {
            anim1.SetBool("Attack", false);
        }

        if (health <= 0)
        {
            if (!dead)
            {
                Death();
                Destroy(gameObject, 0.5f);
            }
            
        }
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        // Debug.Log("damage Taken");
    }

    void Death()
    {
        dead = true;
        anim1.SetTrigger("Death");
        Player.Instance.GoBattle();
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

    public void GiveDamage()
    {
        Health.Instance.TakeDamage(dmg);
    }

    public void Shoot() 
    {
        GetComponentInChildren<FireballSpawner>().FireBullet();
    }


}
