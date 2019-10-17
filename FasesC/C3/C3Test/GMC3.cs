using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMC3 : MonoBehaviour {
    public static GMC3 InstanceGMC3;

    public GameObject ponta;
    public bool spawn = false, auxBol = true, auxBol1 = true;
    public bool invisivel = false;
    public float qntInvi = 1f;
   

    void Start () {
        InstanceGMC3 = this;

    }
	
	
	void Update () {
        if (spawn)
        {
            GameObject auxPonta = Instantiate(ponta, GameObject.Find("Canvas").transform);
            auxPonta.transform.position = new Vector3(10.7f, -56.0f, 2252.0f);
            spawn = false;
        }

        if (invisivel)
        {
            qntInvi -= 0.001f;
        }

        if (qntInvi <= 0 && auxBol1)
        {
            PlayerC3.InstancePlayC3.capsCol.enabled = true;
            PlayerC3.InstancePlayC3.auxBool = true;

            auxBol1 = false;
        }
    }
}
