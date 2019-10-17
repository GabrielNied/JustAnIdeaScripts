using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroFaseB1 : MonoBehaviour
{

    private GameObject gameManager, player;
    private float mov = 100f, rotateSpeed = 0.175f, radius = 5f;
    private Vector2 centre;
    private float angle;

    public int escolheMov, escolheTamanho;

    private Rigidbody2D rb2d;

    private bool andou = false;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprite;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        centre = transform.position;
    }

    void Update()
    {
        Movimentacao();
        TrocaSprite();
        if (gameManager.GetComponent<GameManagerFaseB1>().vida <= 0 || gameManager.GetComponent<GameManagerFaseB1>().score >= 1)
        {
            Destroy(this.gameObject);
        }
    }

    void TrocaSprite()
    {
        if (player.transform.localScale.x >= 20)
        {
            spriteRenderer.color = Color.white;
        }
        else if (player.transform.localScale.x >= 8)
        {
            if (escolheTamanho == 1 || escolheTamanho == 2 || escolheTamanho == 3 || escolheTamanho == 4 || escolheTamanho == 5)
            {
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }
        }
        else if (player.transform.localScale.x >= 3)
        {
            if (escolheTamanho == 1 || escolheTamanho == 2 || escolheTamanho == 3)
            {
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }
        }
        else
        {
            spriteRenderer.color = Color.red;
        }         
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void Movimentacao()
    {
        transform.Rotate(Vector3.forward * 100 * Time.deltaTime);

        if (escolheTamanho == 1 || escolheTamanho == 2 || escolheTamanho == 3)
        {
            this.transform.localScale = new Vector2(20, 20);
            spriteRenderer.sprite = sprite[0];
            this.gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
        }
        else if (escolheTamanho == 4 || escolheTamanho == 5)
        {
            this.transform.localScale = new Vector2(35, 35);
            spriteRenderer.sprite = sprite[1];
            this.gameObject.GetComponent<CircleCollider2D>().radius = 0.6f;
            mov = 75f;
        }
        else if (escolheTamanho == 6)
        {
            this.transform.localScale = new Vector2(50, 50);
            spriteRenderer.sprite = sprite[2];
            this.gameObject.GetComponent<CircleCollider2D>().radius = 1.5f;
            mov = 75f;
        }

        if (escolheMov == 1)
        {
            transform.Translate(Vector3.down * Time.deltaTime * mov/100, Space.World);
        }else if(escolheMov == 2)
        {
            transform.Translate(Vector3.up * Time.deltaTime * mov / 100, Space.World);
        }else if (escolheMov == 3)
        {
            transform.Translate(Vector3.right * Time.deltaTime * mov / 100, Space.World);
        }else if (escolheMov == 4)
        {
            transform.Translate(Vector3.left * Time.deltaTime * mov / 100, Space.World);
        }else if (escolheMov == 5)
        {
            if (!andou)
            {
                rb2d.AddForce((player.transform.position - transform.position).normalized * mov *10);
                andou = true;
            }
        }else if (escolheMov == 6)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle) * 2) * radius;
            transform.position = centre + offset;
        } else if (escolheMov == 7)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle) * 2) * radius;
            transform.position = centre + offset *-1;
        }else if (escolheMov == 8)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle) * 3.25f, Mathf.Cos(angle)) * 4;
            transform.position = centre + offset;
        }else if (escolheMov == 9)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle) * 3.25f, Mathf.Cos(angle)) * 4;
            transform.position = centre + offset *-1;
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
