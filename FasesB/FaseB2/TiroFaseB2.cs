using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroFaseB2 : MonoBehaviour
{
    public GameObject particle;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spriteRenderer.isVisible)
        {
            if (collision.gameObject.tag == "TiroEnemy")
            {
                if (this.gameObject.CompareTag("TiroPlayer"))
                {
                    Instantiate(particle, transform.position, transform.rotation);
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }

            if (this.gameObject.CompareTag("TiroEnemy"))
            {
                if (collision.gameObject.tag == "Player")
                {
                    Instantiate(particle, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }

            if (collision.gameObject.tag == "Uranio")
            {
                Instantiate(particle, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
