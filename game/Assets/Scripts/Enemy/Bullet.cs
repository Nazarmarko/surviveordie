
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private  float moveSpeed = 5f;

    [Range(0f,30)]
   public  int damaheToGive;

   private Rigidbody2D rb;

    private Transform target;
    private Vector2 moveDirection;
    void OnEnable()
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
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Stats>().HealthVisualise(damaheToGive);
            Destroy(gameObject);
        }
    }
}
