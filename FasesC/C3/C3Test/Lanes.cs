using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanes : MonoBehaviour {

    public float speed;
    public GameObject ponta;
    
	
	void Start () {
		
	}
	
	
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        

        if(transform.position.z < -1578.6f)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CheckSpawn")
        {
            GMC3.InstanceGMC3.spawn = true;


        }
    }
}
