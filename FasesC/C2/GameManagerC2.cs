using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerC2 : MonoBehaviour {
    public static GameManagerC2 InstanceGMC2;

    public GameObject[] lanes;
    public Transform pai;
    public GameObject arvore, cogumeloTop;
    public float timeSpawn;
    public int cogumelosParaVencer = 0;
    public Text cogumeloText;
    public LevelFlowManager lfw;

    public AudioClip trilha, cogumelo, vagalume;



    public bool spawn1 = false, spawn2 = false, spawn3 = false;


    void Start () {

        InstanceGMC2 = this;
        pai = GameObject.Find("Canvas/Plataformas").GetComponent<Transform>();
        StartCoroutine(SpawnArvore());
        StartCoroutine(SpawnCogumelo());

        SoundManager.instance.musicSource.clip = trilha;
        SoundManager.instance.musicSource.Play();

        lfw = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();


    }
	
	
	void Update () {
        SpawnLanes();

        cogumeloText.text = "Mushroom: " + cogumelosParaVencer + "/10";

        if (cogumelosParaVencer == 10 || Input.GetKeyDown(KeyCode.Delete))
        {
            SoundManager.instance.musicSource.Stop();
            lfw.gameWon(LevelNumbers.C2);
        }
    }

    public void SpawnLanes()
    {
        if (spawn1)
        {
            GameObject auxLane = Instantiate(lanes[0], pai);
            auxLane.transform.position = new Vector2(13.6f, 0);
            spawn1 = false;
        }

        if (spawn2)
        {
            GameObject auxLane = Instantiate(lanes[0], pai);
            auxLane.transform.position = new Vector2(13.6f, 2.9f);
            spawn2 = false;
        }

        if (spawn3)
        {
            GameObject auxLane = Instantiate(lanes[0], pai);
            auxLane.transform.position = new Vector2(13.6f, 6.0f);
            spawn3 = false;
        }
    }

    IEnumerator SpawnArvore()
    {
        while (true)
        {
            float posicaoY = 0;
            int posicao = Random.Range(0, 3);

            if (posicao == 0)
            {
                posicaoY = 2.3f;
            } else if (posicao == 1)
            {
                posicaoY = -0.8f;
            }
            else if (posicao == 2)
            {
                posicaoY = -3.7f;
            }

            GameObject arvoreAux = Instantiate(arvore, GameObject.Find("Canvas").GetComponent<Transform>());
            arvoreAux.transform.position = new Vector2(7.8f, posicaoY);

            yield return new WaitForSeconds(timeSpawn);
        }
    }

    IEnumerator SpawnCogumelo()
    {
        while (true)
        {
            float posicaoY = 0;
            int posicao = Random.Range(0, 3);

            float random = Random.Range(10, 15);

            if (posicao == 0)
            {
                posicaoY = 1.9f;
            }
            else if (posicao == 1)
            {
                posicaoY = -1.3f;
            }
            else if (posicao == 2)
            {
                posicaoY = -4.1f;
            }

            GameObject cogumeloAux = Instantiate(cogumeloTop, GameObject.Find("Canvas").GetComponent<Transform>());
            cogumeloAux.transform.position = new Vector2(7.8f, posicaoY);

            yield return new WaitForSeconds(random);
        }
    }


}
