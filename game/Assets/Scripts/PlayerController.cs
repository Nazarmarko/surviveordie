using System.Collections;
using System.Collections.Generic;
using UnityEngine;   

[RequireComponent( typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.003f;
    Animator anim;
    float curSpeed;
    float lastDirection;

    public float runSpeed = 1.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", runSpeed);
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down                           
        anim.SetFloat("Direction", Input.GetAxisRaw("Horizontal"));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("SlashBool", true);
            anim.SetTrigger("Slash");
        }


        if (Input.GetAxisRaw("Horizontal") < 0) 
        {
            lastDirection = -1;
            anim.SetFloat("IdleDirection", lastDirection);
            //print(anim.GetFloat("IdleDirection"));
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            lastDirection = 1;
            anim.SetFloat("IdleDirection", lastDirection);
           //print(anim.GetFloat("IdleDirection"));
        }


        //lastDirection = anim.IdleDirection;

    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;

        }

        if (horizontal != 0 || vertical != 0) // Check for diagonal movement
        {
            if(Input.GetKey(KeyCode.LeftShift))
        {
                runSpeed = 5.0f;
                print(runSpeed);
            }
            else
            runSpeed = 1;

        }
        else 
        {
            runSpeed = 0;
        }

        



        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void SlashingEND() 
    {
        anim.SetBool("SlashBool", false);
    }
}
