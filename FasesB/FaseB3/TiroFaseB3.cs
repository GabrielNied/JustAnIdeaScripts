using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroFaseB3 : MonoBehaviour
{
    public GameObject particle;

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "TiroEnemy" || collision.gameObject.tag == "TiroPlayer")
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }    

        if (this.gameObject.CompareTag("TiroEnemy"))
        {
            if (collision.gameObject.tag == "Player")
            {
                Instantiate(particle, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

        if (collision.gameObject.tag == "Uranio" || collision.gameObject.tag == "Meteoro")
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Uranio" || collision.gameObject.tag == "Meteoro")
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
