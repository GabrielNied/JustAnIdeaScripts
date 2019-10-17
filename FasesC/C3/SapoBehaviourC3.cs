using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapoBehaviourC3 : MonoBehaviour {

    public Animator animControlSapo;


    void Start () {
        animControlSapo = GetComponent<Animator>();

    }
	
	
	void Update () {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            
            animControlSapo.SetTrigger("Start");
        }
    }
}
