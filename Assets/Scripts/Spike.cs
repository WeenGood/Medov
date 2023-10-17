using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] public AudioSource hitSpikes;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name  == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Hit();
            hitSpikes.Play();
        }
    }
}
