using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFaseB0 : MonoBehaviour {

    [SerializeField]
    private GameObject caixa, canvas, player;

    public bool podeSpawnarNormal = true, SpawnouEsquerda = false, SpawnouDireita = false;

    private float posicao = 50, escalaGravidade = 0.25f;
    public float tempoSpawn = 0;

    void Start () {
        canvas = GameObject.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spawn", 3f, 2f);
        InvokeRepeating("Spawn", 4f, 2f);
        InvokeRepeating("Spawn", 10f, 4f);
        InvokeRepeating("SpawnMirado", 5f, 3f);
        InvokeRepeating("SpawnMirado", 8f, 3f);
        InvokeRepeating("SpawnMirado", 12f, 4f);
    }

    void Update()
    {
        tempoSpawn += Time.deltaTime;
        escalaGravidade += Time.deltaTime /100;

        if (tempoSpawn >= 30 && tempoSpawn < 39)
        {
            podeSpawnarNormal = false;

            if (!SpawnouEsquerda && tempoSpawn >= 31 && tempoSpawn < 33)
            {
                SpawnEsquerda();
            }
            if (!SpawnouDireita && tempoSpawn >= 33 && tempoSpawn < 35)
            {
                SpawnouEsquerda = false;
                SpawnDireita();
            }
            if (tempoSpawn >= 35)
            {
                SpawnouDireita = false;
                podeSpawnarNormal = true;
                float randomTempo = Random.Range(-5f, 10f);
                tempoSpawn = randomTempo;
            }
        }
    }

    public void Spawn()
    {
        if (podeSpawnarNormal)
        {
            float randomScale = Random.Range(caixa.transform.localScale.x - 25, caixa.transform.localScale.x + 25);
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(randomScale, Screen.width - randomScale), Screen.height + 100, Camera.main.farClipPlane / 2));
            GameObject Caixa = Instantiate(caixa) as GameObject;
            Caixa.transform.SetParent(canvas.GetComponent<Transform>(), false);
            Caixa.transform.position = screenPosition;
            Caixa.GetComponent<Rigidbody2D>().gravityScale = escalaGravidade;


          
            Vector2 randomSize = new Vector2(randomScale, randomScale);
            Caixa.transform.localScale = randomSize;
        }
    }

    public void SpawnMirado()
    {
        if (podeSpawnarNormal)
        {
            float randomScale = Random.Range(caixa.transform.localScale.x - 25, caixa.transform.localScale.x + 25);
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(randomScale, Screen.width - randomScale), Screen.height + 100, Camera.main.farClipPlane / 2));
            GameObject Caixa = Instantiate(caixa) as GameObject;
            Caixa.transform.SetParent(canvas.GetComponent<Transform>(), false);
            Caixa.transform.position = screenPosition;
            Caixa.transform.position = new Vector2( player.transform.position.x, Caixa.transform.position.y);
            Caixa.GetComponent<Rigidbody2D>().gravityScale = escalaGravidade;


            Vector2 randomSize = new Vector2(randomScale, randomScale);
            Caixa.transform.localScale = randomSize;
        }
    }

    public void SpawnEsquerda()
    {
        int tamanhoTela = Screen.width / 110;
        SpawnouEsquerda = true;
        posicao = caixa.transform.localScale.x;
        for (int i = 0; i < tamanhoTela-1; i++)
        {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - posicao, Screen.height + posicao, Camera.main.farClipPlane / 2));
            GameObject Caixa = Instantiate(caixa) as GameObject;
            Caixa.transform.position = screenPosition;
            Caixa.transform.SetParent(canvas.GetComponent<Transform>());
            Caixa.GetComponent<Rigidbody2D>().gravityScale = escalaGravidade;

            Vector2 randomSize = new Vector2(25, 25);
            Caixa.transform.localScale = randomSize;
            posicao += caixa.transform.localScale.x*2;
        }
    }

    public void SpawnDireita()
    {
        int tamanhoTela = Screen.width / 110;
        SpawnouDireita = true;
        posicao = caixa.transform.localScale.x;
        for (int i = 0; i < tamanhoTela-1; i++)
        {
            Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(posicao, Screen.height + posicao, Camera.main.farClipPlane / 2));
            GameObject Caixa = Instantiate(caixa) as GameObject;
            Caixa.transform.position = screenPosition;
            Caixa.transform.SetParent(canvas.GetComponent<Transform>());
            Caixa.GetComponent<Rigidbody2D>().gravityScale = escalaGravidade;

            Vector2 randomSize = new Vector2(25, 25);
            Caixa.transform.localScale = randomSize;
            posicao += caixa.transform.localScale.x*2;
        }
    }
}
