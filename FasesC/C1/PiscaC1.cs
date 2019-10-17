using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscaC1 : MonoBehaviour {

    public Color corMin, corMax;

    public float tempo;

    public static PiscaC1 InstancePiscaC1;




    void Start () {
        InstancePiscaC1 = this;
        corMax = new Color(0,0, 0, 0.90f);
        corMin = new Color(0, 0, 0, 0.30f);
        gameObject.SetActive(false);



    }
	
	
	void Update () {
        tempo += Time.deltaTime;
        if (gameObject.activeSelf)
        {
            Color lerpedColor = Color.Lerp(corMin, corMax, Mathf.PingPong(tempo, 0.5f));
            GetComponent<SpriteRenderer>().color = lerpedColor;
        }


    }


   
}
