using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFaseB3 : MonoBehaviour
{

    [SerializeField]
    private GameObject canvas;
    public GameObject meteoroP, meteoroM, meteoroG, alien, uranioP, uranioM, uranioG;
    public bool podeSpawnarNormal = true;

    private int move;
    public float tempoSpawn = 0, width, height;

    private RectTransform screenPosition;
    private Vector2 spawnPos;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        InvokeRepeating("SpawnNormal", 3.16f, 3.16f);
        InvokeRepeating("SpawnMirado", 8.37f, 8.37f);
        InvokeRepeating("SpawnAlien", 20f, 20f);
        InvokeRepeating("SpawnUranio", 1f, 6f);
        screenPosition = canvas.GetComponent<RectTransform>();
        width = screenPosition.rect.width;
        height = screenPosition.rect.height;
    }

    public void SpawnNormal()
    {
        if (podeSpawnarNormal)
        {
            int randomizaPosicao = Random.Range(1, 5);

            if (randomizaPosicao == 1)
            {
                //Cima
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(-200, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 200, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }

            int randomTamanho = Random.Range(1, 7);

            if(randomTamanho == 6) {
                GameObject MeteoroClone = Instantiate(meteoroG) as GameObject;
                MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
                MeteoroClone.transform.localPosition = spawnPos;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheMov = randomizaPosicao;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheTamanho = randomTamanho;
            }
            else if (randomTamanho == 4 || randomTamanho == 5)
            {
                GameObject MeteoroClone = Instantiate(meteoroM) as GameObject;
                MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
                MeteoroClone.transform.localPosition = spawnPos;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheMov = randomizaPosicao;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheTamanho = randomTamanho;
            }
            else
            {
                GameObject MeteoroClone = Instantiate(meteoroP) as GameObject;
                MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
                MeteoroClone.transform.localPosition = spawnPos;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheMov = randomizaPosicao;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheTamanho = randomTamanho;
            }       
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
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(-200, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 200, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }

            int randomTamanho = Random.Range(1, 7);

            if (randomTamanho == 6)
            {
                GameObject MeteoroClone = Instantiate(meteoroG) as GameObject;
                MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
                MeteoroClone.transform.localPosition = spawnPos;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheMov = 5;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheTamanho = randomTamanho;
            }
            else if (randomTamanho == 4 || randomTamanho == 5)
            {
                GameObject MeteoroClone = Instantiate(meteoroM) as GameObject;
                MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
                MeteoroClone.transform.localPosition = spawnPos;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheMov = 5;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheTamanho = randomTamanho;
            }
            else
            {
                GameObject MeteoroClone = Instantiate(meteoroP) as GameObject;
                MeteoroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
                MeteoroClone.transform.localPosition = spawnPos;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheMov = 5;
                MeteoroClone.GetComponent<MeteoroFaseB3>().escolheTamanho = randomTamanho;
            }
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
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height + 200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, -200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(-200, Screen.height / 2, Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 200, Screen.height / 2, Camera.main.farClipPlane / 2));
            }

            GameObject AlienClone = Instantiate(alien) as GameObject;
            AlienClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
            AlienClone.transform.localPosition = spawnPos;
            AlienClone.GetComponent<AlienFaseB3>().escolheMov = randomizaPosicao;
        }
    }

    public void SpawnUranio()
    {
        if (podeSpawnarNormal)
        {
            int randomizaPosicao = Random.Range(1, 5);

            if (randomizaPosicao == 1)
            {
                //Cima
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height + 200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 2)
            {
                //Baixo
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -200, Camera.main.farClipPlane / 2));
            }
            else if (randomizaPosicao == 3)
            {
                //Esquerda
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(-200, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }
            else
            {
                //Direita
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 200, Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
            }

            int randomTamanho = Random.Range(1, 7);

            if (randomTamanho == 6)
            {
                GameObject UranioCloneG = Instantiate(uranioG) as GameObject;
                UranioCloneG.transform.SetParent(canvas.GetComponent<Transform>(), false);
                UranioCloneG.transform.localPosition = spawnPos;
                UranioCloneG.GetComponent<UranioFaseB3>().escolheMov = randomizaPosicao;
                UranioCloneG.GetComponent<UranioFaseB3>().escolheTamanho = randomTamanho;
            }
            else if (randomTamanho == 4 || randomTamanho == 5)
            {
                GameObject UranioCloneM = Instantiate(uranioM) as GameObject;
                UranioCloneM.transform.SetParent(canvas.GetComponent<Transform>(), false);
                UranioCloneM.transform.localPosition = spawnPos;
                UranioCloneM.GetComponent<UranioFaseB3>().escolheMov = randomizaPosicao;
                UranioCloneM.GetComponent<UranioFaseB3>().escolheTamanho = randomTamanho;
            }
            else
            {
                GameObject UranioCloneP = Instantiate(uranioP) as GameObject;
                UranioCloneP.transform.SetParent(canvas.GetComponent<Transform>(), false);
                UranioCloneP.transform.localPosition = spawnPos;
                UranioCloneP.GetComponent<UranioFaseB3>().escolheMov = randomizaPosicao;
                UranioCloneP.GetComponent<UranioFaseB3>().escolheTamanho = randomTamanho;
            }
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
