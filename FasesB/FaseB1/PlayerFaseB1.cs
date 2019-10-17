using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaseB1 : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private SpriteRenderer spriteRenderer;
    private float tempoPisca = 0f, tempoImortal = 0f, rotacao = 250f, impulso = 2.5f, maxvel = 10;

    private bool imortal = false, podeAndar = true, isWrappingX = false, isWrappingY = false;
    private Vector2 posInicial;
    private GameObject gameManager, propulsor;
    public List<GameObject> piscarJuntoPlayer;
    private ParticleSystem ps;
    private ParticleSystem.MainModule psmain;

    public GameObject particleCome, particleDano;

    void Start()
    {
        propulsor = GameObject.Find("Canvas/PlayerFaseB1/Propulsor");
        gameManager = GameObject.Find("GameManager");
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        posInicial = new Vector2(transform.localPosition.x, transform.localPosition.y);

        for (int i = 0; i < transform.childCount -1; i++)
        {
            GameObject filhoPlayer = transform.GetChild(i).gameObject;
            piscarJuntoPlayer.Add(filhoPlayer);
        }
        ps = propulsor.GetComponent<ParticleSystem>();
        psmain = ps.main;
    }

    private void Update()
    {
        tempoImortal += Time.deltaTime;
        tempoPisca += Time.deltaTime;
        psmain.startSize = transform.localScale.x / 20;

        //Dano
        if (imortal && tempoImortal >= 0.5f)
        {
            podeAndar = true;
        }

        if (imortal && tempoImortal >= 2.5f)
        {
            imortal = false;
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
            rb2d.AddForce(diff * impulso / 4);

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
        if (spriteRenderer.isVisible)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (collision.GetComponent<MeteoroFaseB1>().escolheTamanho == 1 || collision.GetComponent<MeteoroFaseB1>().escolheTamanho == 2 || collision.GetComponent<MeteoroFaseB1>().escolheTamanho == 3)
                {
                    this.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
                    Destroy(collision.gameObject);
                    gameManager.GetComponent<GameManagerFaseB1>().score += 0.02f;
                    Instantiate(particleCome, transform.position, transform.rotation);
                    SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB1>().comendoUranio);
                }
                else if (collision.GetComponent<MeteoroFaseB1>().escolheTamanho == 4 || collision.GetComponent<MeteoroFaseB1>().escolheTamanho == 5)
                {
                    if (this.transform.localScale.x < 8)
                    {
                        if (!imortal)
                        {
                            Instantiate(particleDano, transform.position, transform.rotation);
                            rb2d.velocity = Vector3.zero;
                            rb2d.angularVelocity = 0;
                            transform.rotation = Quaternion.identity;
                            transform.localPosition = posInicial;
                            podeAndar = false;
                            propulsor.SetActive(false);
                            gameManager.GetComponent<GameManagerFaseB1>().vida -= 1;
                            SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB1>().destroyPlayer);
                        }
                        Imortal();
                    }
                    else
                    {
                        this.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                        Destroy(collision.gameObject);
                        gameManager.GetComponent<GameManagerFaseB1>().score += 0.03f;
                        Instantiate(particleCome, transform.position, transform.rotation);
                        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB1>().comendoUranio);
                    }
                }
                else if (collision.GetComponent<MeteoroFaseB1>().escolheTamanho == 6)
                {
                    if (this.transform.localScale.x < 20)
                    {
                        if (!imortal)
                        {
                            Instantiate(particleDano, transform.position, transform.rotation);
                            rb2d.velocity = Vector3.zero;
                            rb2d.angularVelocity = 0;
                            transform.rotation = Quaternion.identity;
                            transform.localPosition = posInicial;
                            podeAndar = false;
                            propulsor.SetActive(false);
                            gameManager.GetComponent<GameManagerFaseB1>().vida -= 1;
                            SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB1>().destroyPlayer);
                        }
                        Imortal();
                    }
                    else
                    {
                        this.transform.localScale += new Vector3(0.7f, 0.7f, 0.7f);
                        Destroy(collision.gameObject);
                        gameManager.GetComponent<GameManagerFaseB1>().score += 0.05f;
                        Instantiate(particleCome, transform.position, transform.rotation);
                        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB1>().comendoUranio);
                    }
                }
            }
        }
    }
}