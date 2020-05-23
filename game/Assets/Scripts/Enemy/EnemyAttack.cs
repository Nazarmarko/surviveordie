using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int health;
    [SerializeField]
    private int dmg;
    private Animator anim1;
    public GameObject bloodEffect;



    private void Start()
    {
        anim1 = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Death();
            Destroy(gameObject, 2f);
        }

        //transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
       // Debug.Log("damage Taken");
    }

    void Death()
    {
        anim1.SetTrigger("Death");
    }

    public void GiveDamage() 
    {
        Health.Instance.TakeDamage(dmg);
    }



}
