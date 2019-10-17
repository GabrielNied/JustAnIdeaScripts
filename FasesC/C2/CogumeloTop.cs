using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogumeloTop : MonoBehaviour {

    private float speed = 10;


    void Start () {
		
	}
	
	
	void Update () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -13.6)
        {
            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
