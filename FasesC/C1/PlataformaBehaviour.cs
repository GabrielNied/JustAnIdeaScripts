using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaBehaviour : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
        Move();
        CheckDeath();

    }

    public void Move()
    {
        if(transform.name == "PlataformaC1 1(Clone)")
        {
            transform.Translate(Vector2.left * GameManagerC1.gmC1Ref.speed1 * Time.deltaTime);
        }

        if (transform.name == "PlataformaC1 2(Clone)")
        {
            transform.Translate(Vector2.left * GameManagerC1.gmC1Ref.speed2 * Time.deltaTime);

        }

        if (transform.name == "PlataformaC1 3(Clone)")
        {
            transform.Translate(Vector2.left * GameManagerC1.gmC1Ref.speed3 * Time.deltaTime);
        }
    }

    public void CheckDeath()
    {
        if(transform.position.x <= -12)
        {
            Destroy(gameObject);
        }
    }
}
