using UnityEngine;

public class Frog : MonoBehaviour
{
    public Sprite flatSprite;

    [SerializeField] public AudioSource dieEnemy;
    [SerializeField] public AudioSource diePlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(collision.transform.DotTest(transform, Vector2.down))
            {
                dieEnemy.Play();
                Flatten();
            }
            else
            {
                diePlayer.Play();
                player.Hit();
            }
        }
    }
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovemnt>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
}
