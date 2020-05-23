using System.Collections;
using System.Collections.Generic;
using UnityEngine;   


public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private Vector2 move;

    [Range(1f,40f)]
    public float speedForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
  void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);

    }
    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + move * speedForce * Time.fixedDeltaTime);
    }
}
