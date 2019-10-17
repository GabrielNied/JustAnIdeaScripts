using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaseB0 : MonoBehaviour
{
    public float vel = 10f, tempoPisca = 0f, tempoImortal =0f;

    public bool imortal = false, podeAndar = true;

    private Rigidbody2D rb2d;
    private Vector2 posInicial;
    private GameManagerFaseB0 gameManager;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerFaseB0>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        posInicial = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    void FixedUpdate()
    {
        Movimenta();

        tempoImortal += Time.deltaTime;
        tempoPisca += Time.deltaTime;

        //Dano
        if (imortal && tempoImortal >= 1.5f)
        {
            podeAndar = true;
        }

        if (imortal && tempoImortal >= 2.5f)
        {
            imortal = false;
        }

        if (imortal)
        {
            if (tempoImortal < 1.5f)
            {
                Pisca();
            }
            else
            {
                spriteRenderer.enabled = true;
            }
        }
    }

    void Movimenta()
    {
        if (podeAndar) {
            //Limites Tela
            Vector2 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            Vector2 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.7f, maxScreenBounds.x - 0.7f), Mathf.Clamp(transform.position.y, minScreenBounds.y + 0.7f, maxScreenBounds.y - 0.7f));

            //Movimentacao
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveHorizontal * vel, 0);
        }
    }

    void Pisca()
    {
        if (tempoPisca >= 0.1f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            tempoPisca = 0;
        }
    }

    void Imortal()
    {
        if (!imortal)
        {
            tempoImortal = 0;
            imortal = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!imortal)
            {
                rb2d.velocity = Vector3.zero;
                transform.localPosition = posInicial;
                podeAndar = false;
                gameManager.getDamage();
                gameManager.getPoint();
                Destroy(collision.gameObject);
            }
            Imortal();
        }
    }
}
