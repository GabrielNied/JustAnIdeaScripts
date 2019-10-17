using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaseB2 : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private SpriteRenderer spriteRenderer;
    private float tempoPisca = 0f, tempoImortal = 0f, rotacao = 280f, impulso = 3f, tempoAtira = 0.15f, maxvel = 10;
    private bool imortal = false, podeAndar = true, podeAtirar = true, isWrappingX = false, isWrappingY = false;
    private Vector2 posInicial;
    private GameObject gameManager, canvas, propulsor;
    public GameObject tiroPrefab;
    public List<GameObject> piscarJuntoPlayer;

    void Start()
    {
        propulsor = GameObject.Find("Canvas/PlayerFaseB2/Propulsor");
        canvas = GameObject.Find("Canvas");
        gameManager = GameObject.Find("GameManager");
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        posInicial = new Vector2(transform.localPosition.x, transform.localPosition.y);

        for (int i = 0; i < transform.childCount -1; i++)
        {
            GameObject filhoPlayer = transform.GetChild(i).gameObject;
            piscarJuntoPlayer.Add(filhoPlayer);
        }
    }

    private void Update()
    {
        tempoAtira += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (podeAtirar && tempoAtira >= 0.25f)
            {
                Atira();
                tempoAtira = 0;
            }
        }

        tempoImortal += Time.deltaTime;
        tempoPisca += Time.deltaTime;

        //Dano
        if (imortal && tempoImortal >= 0.5f)
        {
            podeAndar = true;
        }

        if (imortal && tempoImortal >= 2.5f)
        {
            imortal = false;
            podeAtirar = true;
        }

        if (imortal)
        {
            if (tempoImortal < 2.4f)
            {
                Pisca();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.PingPong(tempoImortal * 10, 1.0f));
            }
            else
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                foreach (GameObject filhosPlayer in piscarJuntoPlayer)
                {
                    filhosPlayer.SetActive(true);
                }
            }
        }
    }

    void FixedUpdate()
    {
        Movimenta();
        ScreenWrap();
    }

    void Movimenta()
    {
        if (podeAndar)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.angularVelocity = -moveHorizontal * rotacao;

            if (rb2d.velocity.sqrMagnitude < maxvel)
            {
                float moveVertical = Input.GetAxis("VerticalFaseB2");
                rb2d.AddForce(transform.up * moveVertical * impulso);
            }

            //Forca Contraria
            Vector2 vMovement = rb2d.velocity.normalized;
            Vector2 vFacing = transform.forward;
            Vector2 diff = vFacing - vMovement;
            rb2d.AddForce(diff * impulso/4);

            if (Input.GetAxis("VerticalFaseB2") != 0)
            {
                propulsor.SetActive(true);
            }
            else
            {
                propulsor.SetActive(false);

            }
        }
    }

    void Atira()
    {
        GameObject TiroClone = Instantiate(tiroPrefab, transform.localPosition, transform.rotation) as GameObject;
        TiroClone.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000);
        TiroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);

        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB2>().tiro);
    }

    void ScreenWrap()
    {
        bool isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 newPosition = transform.position;

        if (newPosition.x > 1 || newPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        if (newPosition.y > 1 || newPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }

        transform.position = newPosition;
    }

    bool CheckRenderers()
    {
        if (spriteRenderer.isVisible)
        {
            return true;
        }
        return false;
    }

    void Pisca()
    {
        if (tempoPisca >= 0.1f)
        {
            foreach (GameObject filhosPlayer in piscarJuntoPlayer)
            {
                filhosPlayer.SetActive(false);
                propulsor.SetActive(false);
            }
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "TiroEnemy")
        {
            if (!imortal)
            {
                rb2d.velocity = Vector3.zero;
                rb2d.angularVelocity = 0;
                transform.rotation = Quaternion.identity;
                transform.localPosition = posInicial;
                podeAndar = false;
                podeAtirar = false;
                propulsor.SetActive(false);
                gameManager.GetComponent<GameManagerFaseB2>().vida -= 1;

                SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB2>().destroyPlayer);
            }
            Imortal();
        }        


    }
}