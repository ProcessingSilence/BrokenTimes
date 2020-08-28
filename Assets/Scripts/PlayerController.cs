using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    public Vector2 curMov;

    [SerializeField] private LayerMask platformLayerMask;
    // Jump
    public float jumpVel;
    public float fallMult = 2.5f;
    public float lowJumpMult = 2f;
    private Vector2 jumpVec;
    
    // Movement 
    public float moveSpeed;
    private float movVec;
    
    // Components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    
    // Death Projectile Components
    public Transform DeathProjectileLocation;
    public GameObject DeathProjectile;

    // Animator variables
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int CurrentJump = Animator.StringToHash("currentJump");

    // Sounds
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip landSound;
    private bool playLandSound;
    
    // Death
    public int regularWall;
    public int barWall;
    public float fallDeathPos;
    public int deathFlag;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= fallDeathPos)
        {
            deathFlag = 1;
        }

        if (deathFlag == 1)
        {
            deathFlag = 2;
            Instantiate(DeathProjectile, DeathProjectileLocation.position, Quaternion.identity);
        }
    }


    private void Update()
    {
        // Crushed between walls
        if (regularWall == 1 && barWall == 1)
        {
            regularWall = barWall = 2;
            deathFlag = 1;
        }
        
        if (deathFlag == 2)
        {
            gameObject.SetActive(false);
        }
        Jumping();
        Movement();
        rb.velocity = new Vector2(movVec, rb.velocity.y);
    }

    void Jumping()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
            rb.velocity = Vector2.up * jumpVel;
            animator.SetInteger(CurrentJump, 1);
        }
        if (IsGrounded() && animator.GetInteger(CurrentJump) == 2)
        {
            animator.SetInteger(CurrentJump, 0);
        }
        
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        }

        if (IsGrounded() == false)
        {
            playLandSound = true;
        }
    }

    void Movement()
    {
        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                movVec = moveSpeed;
                if (spriteRenderer.flipX)
                    spriteRenderer.flipX = false;
                //Debug.Log("right");
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                movVec = -moveSpeed;
                if (spriteRenderer.flipX == false)
                    spriteRenderer.flipX = true;
                //Debug.Log("left");
            }
        }
        else if (!Input.GetButton("Horizontal"))
        {
            movVec = 0;
        }
        animator.SetFloat(MoveSpeed, movVec);
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center,boxCollider2D.bounds.size, 0f,Vector2.down, 0.2f, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            if (playLandSound)
            {
                playLandSound = false;
                audioSource.clip = landSound;
                audioSource.Play();
            }
        }
        else
        {
            rayColor = Color.red;
            if (animator.GetInteger(CurrentJump) == 1)
            {
                animator.SetInteger(CurrentJump, 2);
            }
        }

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ouch"))
        {
            deathFlag = 1;
            Destroy(boxCollider2D);
        }
        if (other.gameObject.CompareTag("Wall") && deathFlag < 1)
        {
            regularWall = 1;
        }
        if (other.gameObject.CompareTag("BarWall")&& deathFlag < 1)
        {
            barWall = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") && deathFlag < 1)
        {
            regularWall = 0;
        }
        if (other.gameObject.CompareTag("BarWall") && deathFlag < 1)
        {
            barWall = 0;
        }
    }
}
