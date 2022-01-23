using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    //[SerializeField] float speed = 1.0f;
    //[SerializeField] float jumpForce = 4.0f; 
    //public float inputX;
    //private bool combatIdle = false;
    private bool isGrounded = true;

    Rigidbody2D rb;
    Animator animator;

	// Use this for initialization
	void Start () {        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {

 
        //movement with animation control
        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector2(3, rb.velocity.y);
            transform.localScale = new Vector3(-3.0f, 3.0f, 1.0f);
            animator.SetInteger("AnimState", 2);

        }
        else if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-3, rb.velocity.y);
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
            animator.SetInteger("AnimState", 2);
        }
        else
        {
            animator.SetInteger("AnimState", 1);
        }


        //Jumping with animation control
        isGrounded= IsGrounded();
        animator.SetBool("Grounded", isGrounded);

        if (Input.GetKeyDown("w") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            animator.SetTrigger("Jump");

        }


    }


    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.03f);
    }

}
