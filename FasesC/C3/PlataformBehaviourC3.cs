using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformBehaviourC3 : MonoBehaviour {

    private float speed = 50;
	
	void Start () {
		
	}
	
	
	void Update () {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(transform.position.x < -283.3f)
        {
            Destroy(gameObject);
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform.tag == "CheckSpawn")
        {
            GameManagerC3.InstanceGMC3.spawn = true;
        }

        if (other.transform.tag == "CheckSpawn2")
        {
            GameManagerC3.InstanceGMC3.spawn2 = true;
        }

        if (other.transform.tag == "CheckSpawn3")
        {
            GameManagerC3.InstanceGMC3.spawn3 = true;
        }
    }
}
