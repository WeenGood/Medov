using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    [SerializeField] private float speed;
    private int XMoveDirection = 1;
    private SpriteRenderer sprite;

    [SerializeField] AudioSource audioSourceEnemyDie;

    private bool shouldDie = false;
    private float deathTimer = 0f;
    public float timeBeforeDestroy = 0f;
    
    [SerializeField] public LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateEnemyPosition();
        checkDie();
        if(!shouldDie) hitPlayer();
        
    }

    void updateEnemyPosition()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(XMoveDirection*speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(XMoveDirection*speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.transform.position) < 0.5f && currentPoint == pointB.transform)
        {
            sprite.flipX = true;
            XMoveDirection = -1;
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.transform.position) < 0.5f && currentPoint == pointA.transform)
        {
            sprite.flipX = false;
            XMoveDirection = 1;
            currentPoint = pointB.transform;
        }
    }

    public void dieEnemy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        audioSourceEnemyDie.Play();
        anim.SetTrigger("death");
        shouldDie = true;
    }

    void checkDie ()
    {
        if(shouldDie)
        {
            if (deathTimer <= timeBeforeDestroy)
            {
                deathTimer += Time.deltaTime;
            }
            else
            {
                shouldDie = false;
                Destroy(this.gameObject);
            }
        }
    }

    private void hitPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0), 1f, playerMask);
        if (hit!= null && hit.collider != null && hit.distance < 1.1f && hit.collider.gameObject.name == "Player")
        {
            hit.collider.gameObject.GetComponent<Player_Life>().playerDie();
        }
    }

}
