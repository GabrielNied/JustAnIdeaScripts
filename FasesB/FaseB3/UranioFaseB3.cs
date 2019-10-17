using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranioFaseB3 : MonoBehaviour
{

    private GameObject gameManager, player;
    private float mov = 750f;

    public int escolheMov = 0, escolheTamanho = 0;

    private Rigidbody rb;

    private bool andou = false;

    private Vector3 rotacaoInicial;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        float randomRotacaoX = Random.Range(0.1f, 1);
        float randomRotacaoY = Random.Range(0.1f, 1);
        float randomRotacaoZ = Random.Range(0.1f, 1);
        rotacaoInicial = new Vector3(randomRotacaoX, randomRotacaoY, randomRotacaoZ);
    }

    void Update()
    {

        if (gameManager.GetComponent<GameManagerFaseB3>().vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        Movimentacao();
    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void Movimentacao()
    {
        transform.Rotate(rotacaoInicial * 100 * Time.deltaTime);

        if (escolheTamanho == 1 || escolheTamanho == 2 || escolheTamanho == 3)
        {
            mov = 500f;
        }
        else if (escolheTamanho == 4 || escolheTamanho == 5)
        {
            mov = 400f;
        }
        else if (escolheTamanho == 6)
        {
            mov = 300f;
        }

        if (escolheMov == 1)
        {
            if (!andou)
            {
                rb.AddForce(Vector3.down.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 2)
        {
            if (!andou)
            {
                rb.AddForce(Vector3.up.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 3)
        {
            if (!andou)
            {
                rb.AddForce(Vector3.right.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 4)
        {
            if (!andou)
            {
                rb.AddForce(Vector3.left.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 5)
        {
            if (!andou)
            {
                rb.AddForce((player.transform.position - transform.position).normalized * mov);
                andou = true;
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
