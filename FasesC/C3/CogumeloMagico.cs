using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogumeloMagico : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
        transform.Translate(Vector3.left * 70 * Time.deltaTime);
    }
}
