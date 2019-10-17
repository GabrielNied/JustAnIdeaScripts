using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncoBehaviour : MonoBehaviour {

    public float speed;


    void Start()
    {

    }


    void Update()
    {
        

        if (transform.position.x < -13.6)
        {
            Destroy(gameObject);
        }

        if(transform.tag == "Cogumelo")
        {
            transform.Translate(Vector2.left * -6 * Time.deltaTime);
        }

        if (transform.tag != "Cogumelo")
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }


    }
}
