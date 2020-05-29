using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 5f;

    [SerializeField]
    private int dmg;

    Rigidbody2D rb;

    private Transform target;
    Vector2 moveDirection;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed; 
        Destroy(gameObject, 10f);
    }
     void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Debug.Log("Hit!");
            Health.Instance.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }

    void GiveDamage()
    {
        Health.Instance.TakeDamage(dmg);
    }

}
