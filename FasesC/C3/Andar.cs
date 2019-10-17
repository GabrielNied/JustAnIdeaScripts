using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Andar : MonoBehaviour {

    private float _speed = 50;
    private BoxCollider boxCol;


    void Start () {

		if(boxCol != null)
        {
            boxCol = GetComponent<BoxCollider>();
        }
	}
	
	
	void Update () {


        transform.Translate(Vector3.left * _speed * Time.deltaTime);
      
        

        if (transform.position.x < -85f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Tronco")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Cogumelo")
        {
            Destroy(gameObject);
        }
    }
}
