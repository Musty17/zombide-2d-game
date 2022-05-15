using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public float speed = 1.0f;
    public float moveDirection;

    private bool jump;
    private bool grounded = true;
    private bool moving;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D rb2d;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>(); // caching animator
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (rb2d.velocity != Vector2.zero) 
        {
            moving = true;
        } 
        else 
        { 
            moving = false; //herhangi bir hýzým varsa hareket ettiðim pozisyondayým.
        }
        rb2d.velocity = new Vector2(speed * moveDirection, rb2d.velocity.y);
        if (jump == true) 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            jump = false;
        }
    }
    void Update()
    {
        if (grounded == true && (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D))))
        {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed", speed);
            } 
            else if (Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed", speed);
            }
        }
        else if (grounded == true)  // hiçbir tuþa basmýyorsam yerdeysem
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed", 0.0f);
        }

        if (grounded == true && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // baðlý olduðu obje baþka bir collision objeyle çarpýþýrsa bu event çalýþýr.
    {
        if (collision.gameObject.CompareTag("Zemin")) 
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
    }
}
