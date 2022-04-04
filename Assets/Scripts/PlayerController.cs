using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float thrust;
    public float movespeed = 1f;
    public float fastspeed = 2f;
    public float realspeed;
    public float jumpforce = 150f;
    public Vector2 moveVector;
    public Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        realspeed = movespeed;
    }
        void Update()
    {   
        // walk();
        // run();
        // jump();
    }
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * thrust);
        CheckGround();
        SquatCheck();
        walk();
        run();
        jump();
    }

    void walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * realspeed,rb.velocity.y);

    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            //rb.velocity = new Vector2(rb.velocity.x,jumpforce);
            rb.AddForce(Vector2.up*jumpforce);
        }
    }

    public bool onGround;
    
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    void CheckGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }

    public Transform TopCheck;
    public float topCheckRadius = 0.3f;
    public LayerMask Roof;
    public Collider2D poseStand;
    public Collider2D poseSquat;

    void SquatCheck()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Physics2D.OverlapCircle(TopCheck.position , topCheckRadius , Roof))
        {
            poseStand.enabled = false;
            poseSquat.enabled = true;

        }
        
        else if (!Physics2D.OverlapCircle(TopCheck.position , topCheckRadius , Roof))
        {
            poseStand.enabled = true;
            poseSquat.enabled = false;
        }         
    }

    void run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realspeed = fastspeed;
        }
        else
        {
            realspeed = movespeed;
        }
    }
}
