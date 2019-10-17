using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerC3 : MonoBehaviour {
    public static GameManagerC3 InstanceGMC3;

    public bool spawn = false, spawn2 = false, spawn3 = false, invisivel = false, auxBool2 = true;
    public GameObject[] plataformas;
    public GameObject sapo, tronco, cogumelo;
    public float qntInvi = 1;
    public int cogumelosForWin;
    public Text mushroomText;
    public LevelFlowManager lfw;
    public AudioClip trilha, toco, getCogumelo, ambiente;




    void Start () {
        InstanceGMC3 = this;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnEnemyAll());
        StartCoroutine(SpawnTronco());
        StartCoroutine(SpawnCogumelo());

        SoundManager.instance.musicSource.clip = trilha;
        SoundManager.instance.musicSource.Play();

        SoundManager.instance.ambienteSource.clip = trilha;
        SoundManager.instance.ambienteSource.Play();

        lfw = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();      
    }
	
	
	void Update () {

        SpawnPlataformas();

        if (invisivel)
        {
            qntInvi -= 0.001f;
        }

        if(qntInvi <= 0 && auxBool2)
        {
            
            VagalundoC3.InstancePlayer.invAindaPode = false;
            auxBool2 = false;
        }

        mushroomText.text = "Mushroom: " + cogumelosForWin + "/3";

        if (cogumelosForWin == 3 || Input.GetKeyDown(KeyCode.Delete))
        {
            SoundManager.instance.musicSource.Stop();
            SoundManager.instance.ambienteSource.Stop();
            lfw.gameWon(LevelNumbers.C3);
        }     
    }

    public void SpawnPlataformas()
    {
        if (spawn)
        {
            GameObject auxSpawn1 = Instantiate(plataformas[0], GameObject.Find("Canvas").GetComponent<Transform>());
            auxSpawn1.transform.position = new Vector3(91.1f, 13.1f, 0.0f);
            spawn = false;
        }

        if (spawn2)
        {
            GameObject auxSpawn2 = Instantiate(plataformas[1], GameObject.Find("Canvas").GetComponent<Transform>());
            auxSpawn2.transform.position = new Vector3(91.1f, -23.2f, 0.0f);
            spawn2 = false;
        }

        if (spawn3)
        {
            GameObject auxSpawn3 = Instantiate(plataformas[2], GameObject.Find("Canvas").GetComponent<Transform>());
            auxSpawn3.transform.position = new Vector3(91.9f, -56.2f, 0.0f);
            spawn3 = false;
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float random = Random.Range(5, 7);
            GameObject auxSapo = Instantiate(sapo, GameObject.Find("Canvas").GetComponent<Transform>());
            float randomPos = Random.Range(1, 4);
            
            if(randomPos == 1)
            {
                auxSapo.transform.position = new Vector3(91.9f, 15.3f, 0.0f);
            }
            else if(randomPos == 2)
            {
                auxSapo.transform.position = new Vector3(91.1f, -20.8f, 0.0f);
            }
            else if (randomPos == 3)
            {
                auxSapo.transform.position = new Vector3(91.1f, -54.1f, 0.0f);
            }

            yield return new WaitForSeconds(random);
        }
    }

    IEnumerator SpawnEnemyAll()
    {
        while (true)
        {
            float random2 = Random.Range(7, 10);
            GameObject auxSapo1 = Instantiate(sapo, GameObject.Find("Canvas").GetComponent<Transform>());
            GameObject auxSapo2 = Instantiate(sapo, GameObject.Find("Canvas").GetComponent<Transform>());
            GameObject auxSapo3 = Instantiate(sapo, GameObject.Find("Canvas").GetComponent<Transform>());
            auxSapo1.transform.position = new Vector3(91.9f, -54.1f, 0.0f);
            auxSapo2.transform.position = new Vector3(91.1f, -20.8f, 0.0f);
            auxSapo3.transform.position = new Vector3(91.1f, 15.3f, 0.0f);
            yield return new WaitForSeconds(random2);
        }
    }

    IEnumerator SpawnTronco()
    {
        while (true)
        {
            float random3 = Random.Range(2, 4);
            float randomPos1 = Random.Range(1, 4);
            GameObject auxTronco = Instantiate(tronco, GameObject.Find("Canvas").GetComponent<Transform>());

            if (randomPos1 == 1)
            {
                auxTronco.transform.position = new Vector3(91.9f, 15.1f, 0.0f);
            }
            else if (randomPos1 == 2)
            {
                auxTronco.transform.position = new Vector3(91.1f, -20.8f, 0.0f);
            }
            else if (randomPos1 == 3)
            {
                auxTronco.transform.position = new Vector3(91.1f, -54f, 0.0f);
            }

            yield return new WaitForSeconds(random3);

        }
    }

    IEnumerator SpawnCogumelo()
    {
        while (true)
        {
            float random4 = Random.Range(30, 80);
            float randomPos3 = Random.Range(1, 4);
            GameObject auxCogumelo = Instantiate(cogumelo, GameObject.Find("Canvas").GetComponent<Transform>());

            if (randomPos3 == 1)
            {
                auxCogumelo.transform.position = new Vector3(91.9f, 15.1f, 0.0f);
            }
            else if (randomPos3 == 2)
            {
                auxCogumelo.transform.position = new Vector3(91.1f, -20.8f, 0.0f);
            }
            else if (randomPos3 == 3)
            {
                auxCogumelo.transform.position = new Vector3(91.1f, -54f, 0.0f);
            }

            yield return new WaitForSeconds(random4);
        }
    }
}
