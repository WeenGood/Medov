using UnityEngine;

public class EntityMovemnt : MonoBehaviour
{
    public float speed = 1f;
    private new Rigidbody2D rigidbody;
    public Vector2 direction = Vector2.left;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        if (rigidbody.Raycast(direction, 0.375f, 0.5f))
        {
            direction = -direction;
        }
        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y =Mathf.Max(velocity.y, 0f);
        }
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            direction = -direction;
        }
    }
}
