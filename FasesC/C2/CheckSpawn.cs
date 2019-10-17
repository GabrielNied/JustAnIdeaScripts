using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpawn : MonoBehaviour {

    public GameObject laneMaior;

	void Start () {
        

    }
	
	
	void Update () {

        
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.tag == "CheckSpawn")
        {
            GameManagerC2.InstanceGMC2.spawn1 = true;
            
        }
        else if (other.tag == "CheckSpawn2")
        {
            GameManagerC2.InstanceGMC2.spawn2 = true;
            
        }
        else if (other.tag == "CheckSpawn3")
        {
            GameManagerC2.InstanceGMC2.spawn3 = true;
            

        }
    }



    

    
}
