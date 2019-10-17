using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFaseB2 : MonoBehaviour
{

    [SerializeField]
    private GameObject canvas;
    public GameObject meteoro, alien;
    public bool podeSpawnarNormal = true;

    private int move;
    public float tempoSpawn = 0;

    private Vector2 screenPosition;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        InvokeRepeating("SpawnNormal", 3.16f, 3.16f);
        InvokeRepeating("SpawnMirado", 8.37f, 8.37f);
        InvokeRepeating("SpawnAlien", 20.52f, 20.52f);
    }

    public void SpawnNormal()
    {
        if (podeSpawnarNormal)
        {
            int randomizaPosicao = Random.Range(1, 5);

            if (randomizaPosicao == 1)
            {
                //Cima
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 50, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -50, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(-50, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 50, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }

            GameObject MeteoroClone = Instantiate(meteoro) as GameObject;
            MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
            MeteoroClone.transform.position = screenPosition;
            MeteoroClone.GetComponent<MeteoroFaseB2>().escolheMov = randomizaPosicao;
            int randomTamanho = Random.Range(1, 7);
            MeteoroClone.GetComponent<MeteoroFaseB2>().escolheTamanho = randomTamanho;

        }
    }

    public void SpawnMirado()
    {
        if (podeSpawnarNormal)
        {
            int randomizaPosicao = Random.Range(1, 5);

            if (randomizaPosicao == 1)
            {
                //Cima
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 50, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -50, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(-50, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 50, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }
            GameObject MeteoroClone = Instantiate(meteoro) as GameObject;
            MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
            MeteoroClone.transform.position = screenPosition;
            MeteoroClone.GetComponent<MeteoroFaseB2>().escolheMov = 5;
            int randomTamanho = Random.Range(1, 7);
            MeteoroClone.GetComponent<MeteoroFaseB2>().escolheTamanho = randomTamanho;
        }
    }

    public void SpawnAlien()
    {
        if (podeSpawnarNormal)
        {
            int randomizaPosicao = Random.Range(1, 5);

            if (randomizaPosicao == 1)
            {
                //Cima
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + 50, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, -50, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(-50, Screen.height / 2, Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 50, Screen.height / 2, Camera.main.farClipPlane / 2));
            }
            GameObject AlienClone = Instantiate(alien) as GameObject;
            AlienClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
            AlienClone.transform.position = screenPosition;
            AlienClone.GetComponent<AlienFaseB2>().escolheMov = randomizaPosicao;
        }
    }

}

/* MOV 1 = CIMA - BAIXO
 * MOV 2 = BAIXO - CIMA
 * MOV 3 = LADO ESQUERDO - DIREITO
 * MOV 4 = LADO DIREITO - ESQUERDO
 * MOV 5 = MIRA PLAYER
 * MOV 6 = CIRCULAR
 * */
