using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerC0 : MonoBehaviour {

    public static UIManagerC0 InstanciaUI;
    public Image invBar;

    
    public GameObject[] life;
    public bool Morreu = false;


    void Start () {
        InstanciaUI = this;
        life = new GameObject[3];

        life[0] = GameObject.FindWithTag("Vida1C0");
        life[1] = GameObject.FindWithTag("Vida2C0");
        life[2] = GameObject.FindWithTag("Vida3C0");
        if(invBar != null)
        {
            invBar = GameObject.Find("InvBar").GetComponent<Image>();
        }
        

    }
	
	
	void Update () {
        if (GameManagerC1.gmC1Ref != null && invBar != null)
        {
            invBar.fillAmount = GameManagerC1.gmC1Ref.qntInvi;
        }

        if(GameManagerC3.InstanceGMC3 != null && invBar != null)
        {
            invBar.fillAmount = GameManagerC3.InstanceGMC3.qntInvi;
        }
        

    }

    public void GetDamage()
    {
        if(life[2].activeSelf == true)
        {
            life[2].SetActive(false);
           
        }
        else if(life[2].activeSelf == false && life[1].activeSelf == true)
        {
            life[1].SetActive(false);
        }
        else if(life[1].activeSelf == false && life[0].activeSelf == true)
        {
            life[0].SetActive(false);
            Morreu = true;
        }
    }

}
