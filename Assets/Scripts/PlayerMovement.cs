using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask notPlayerMask;


    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;


    private int cherries;
    private TMP_Text cherriesText;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cherries = GetComponent<ItemCollector>().cherries;
        cherriesText = GetComponent<ItemCollector>().cherriesText;
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        PlayerRaycast();
        

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    

    private void PlayerRaycast()
    {
        
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.1f, notPlayerMask);
        if (hitUp != null && hitUp.collider != null && hitUp.distance < 0.9f && hitUp.collider.tag == "LootBox")
        {

        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, notPlayerMask);
        if (hit != null && hit.collider != null && hit.distance < 1.1f && hit.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            hit.collider.GetComponent<EnemyPatrol>().dieEnemy();//Destroy(hit.collider.gameObject);
        }
    }


}
