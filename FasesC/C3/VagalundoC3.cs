using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagalundoC3 : MonoBehaviour {
    public static VagalundoC3 InstancePlayer;

    public int posicao = 2, opcoes = 3;
    public Transform[] positions;
    public bool auxBool = true, piscando = false, invAindaPode = true, boolEntrou = true, imortal;
    public GameObject particleNuvem2, particleNuvem3;
    public float tempo;

    public Light lightDoVagalume;
    public Material materialVagalume, materialInv;
    public GameObject pegarMaterial;


    void Start () {
        InstancePlayer = this;
        lightDoVagalume.enabled = true;
    }
	
	
	void Update () {
        Move();
        Inv();      


        if (piscando)
        {
            if (tempo >= 2)
            {
                imortal = false;
                boolEntrou = true;
                piscando = false;

            }
        }
        tempo += Time.deltaTime;


        if (UIManagerC0.InstanciaUI.Morreu)
        {
            GameManagerC3.InstanceGMC3.lfw.gameLost();
            SoundManager.instance.musicSource.Stop();
            SoundManager.instance.ambienteSource.Stop();
            Destroy(gameObject);
        }

    }

    public void Move()
    {
        Teleportar();

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (posicao < opcoes)
            {
                posicao++;
                Teleportar();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (posicao > 1)
            {
                posicao--;
                Teleportar();
            }
        }
    }

    public void Teleportar()
    {
        if (posicao == 1)
        {
            transform.position = positions[0].transform.position;
        }
        else if (posicao == 2)
        {
            transform.position = positions[1].transform.position;
        }
        else if (posicao == 3)
        {
            transform.position = positions[2].transform.position;
        }
    }

    public void Inv()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {         
            if (auxBool && invAindaPode)
            {
                imortal = true;
                
                Instantiate(particleNuvem2, transform);
                Destroy(GameObject.Find("ParticulaNuvem2(Clone)"), 2);
                GameManagerC3.InstanceGMC3.invisivel = true;

                pegarMaterial.GetComponent<SkinnedMeshRenderer>().material = materialInv;

                //lightDoVagalume.range = 30;
                //lightDoVagalume.color = new Color(0, 0, 225);
                lightDoVagalume.enabled = false;

                auxBool = false;
            }
            else if(boolEntrou)
            {
                imortal = false;
                GameManagerC3.InstanceGMC3.invisivel = false;

                Instantiate(particleNuvem3, transform);
                Destroy(GameObject.Find("ParticulaNuvem3(Clone)"), 2);

                pegarMaterial.GetComponent<SkinnedMeshRenderer>().material = materialVagalume;
                //lightDoVagalume.range = 12;
                //lightDoVagalume.color = new Color(175, 225, 0);
                lightDoVagalume.enabled = true;

                auxBool = true;
            }
            
        }
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.transform.tag == "Enemy")
        {
            if (!imortal)
            {
                UIManagerC0.InstanciaUI.GetDamage();

                imortal = true;
                boolEntrou = false;
                posicao = 2;
                tempo = 0;
                piscando = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tronco")
        {
            if (!imortal)
            {
                UIManagerC0.InstanciaUI.GetDamage();
                SoundManager.instance.RandomizeSfx(GameManagerC3.InstanceGMC3.toco);

                imortal = true;
                boolEntrou = false;
                posicao = 2;
                tempo = 0;
                piscando = true;
            }
        }

        if(other.tag == "Cogumelo")
        {            
            Destroy(other.gameObject);
            GameManagerC3.InstanceGMC3.cogumelosForWin++;
            SoundManager.instance.RandomizeSfx(GameManagerC3.InstanceGMC3.getCogumelo);
        }
    }
}
