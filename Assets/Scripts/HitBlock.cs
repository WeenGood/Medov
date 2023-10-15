using UnityEngine;
using System.Collections;
public class HitBlock : MonoBehaviour
{
    public GameObject item;
    public int maxHits = 1;
    public Sprite emptyBlock;
    private bool animating;

    [SerializeField] private AudioSource getPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
                getPoint.Play();
                collision.gameObject.GetComponent<ItemCollector>().updateScore();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item,transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());

    }
    

    private IEnumerator Animate()
    {
        animating = true;

        
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up*0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;
        
        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }
}
