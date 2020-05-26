
using UnityEngine;   


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private Vector2 move;

    [Range(0.1f, 20f)]
    public float speedForce, accelerateForce;
    
    private float forceNormilized;
    void OnEnable()
    {
        forceNormilized = speedForce;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
  void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift)) { speedForce = accelerateForce; }else { speedForce = forceNormilized; }

        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.sqrMagnitude);
    }
    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + move * speedForce * Time.fixedDeltaTime);
    }
}
