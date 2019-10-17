using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroFaseB2 : MonoBehaviour
{

    private GameObject gameManager, player, canvas;
    private float mov = 100f;

    public int escolheMov = 0, escolheTamanho = 0;

    private Rigidbody2D rb2d;

    private bool andou = false;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprite;

    public GameObject meteoro, particle;

    private Vector2 screenPosition;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (gameManager.GetComponent<GameManagerFaseB2>().vida <= 0 || gameManager.GetComponent<GameManagerFaseB2>().score >= 1)
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
        transform.Rotate(Vector3.forward * 100 * Time.deltaTime);

        if (escolheTamanho == 1 || escolheTamanho == 2 || escolheTamanho == 3)
        {
            this.transform.localScale = new Vector2(20, 20);
            spriteRenderer.sprite = sprite[0];
            this.gameObject.GetComponent<CircleCollider2D>().radius = 0.75f;
        }
        else if (escolheTamanho == 4 || escolheTamanho == 5)
        {
            this.transform.localScale = new Vector2(35, 35);
            spriteRenderer.sprite = sprite[1];
            this.gameObject.GetComponent<CircleCollider2D>().radius = 1f;
            mov -= 25f;
        }
        else if (escolheTamanho == 6)
        {
            this.transform.localScale = new Vector2(50, 50);
            spriteRenderer.sprite = sprite[2];
            this.gameObject.GetComponent<CircleCollider2D>().radius = 1.6f;
            mov -= 50f;
        }

        if (escolheMov == 1)
        {
            if (!andou)
            {
                rb2d.AddForce(Vector3.down.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 2)
        {
            if (!andou)
            {
                rb2d.AddForce(Vector3.up.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 3)
        {
            if (!andou)
            {
                rb2d.AddForce(Vector3.right.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 4)
        {            
            if (!andou)
            {
                rb2d.AddForce(Vector3.left.normalized * mov);
                andou = true;
            }
        }
        else if (escolheMov == 5)
        {
            if (!andou)
            {
                rb2d.AddForce((player.transform.position - transform.position).normalized * mov * 10);
                andou = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spriteRenderer.isVisible)
        {
            if (collision.gameObject.tag == "TiroPlayer" || collision.gameObject.tag == "TiroEnemy" || collision.gameObject.tag == "Player")
            {
                if (escolheTamanho == 6)
                {
                    GameObject MeteoroClone1 = Instantiate(meteoro, transform.localPosition, transform.rotation) as GameObject;
                    GameObject MeteoroClone2 = Instantiate(meteoro, transform.localPosition, transform.rotation) as GameObject;
                    if (this.rb2d.velocity.x > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);

                    }
                    else if (this.rb2d.velocity.x < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);
                    }
                    else if (this.rb2d.velocity.y > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(-this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                    }
                    else if (this.rb2d.velocity.y < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(-this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                    }
                    MeteoroClone1.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone1.GetComponent<MeteoroFaseB2>().escolheTamanho = 4;
                    MeteoroClone2.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone2.GetComponent<MeteoroFaseB2>().escolheTamanho = 4;
                    Instantiate(particle, transform.position, transform.rotation);
                    SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB2>().destroyMeteoro);
                }
                else if (escolheTamanho == 4 || escolheTamanho == 5)
                {
                    GameObject MeteoroClone1 = Instantiate(meteoro, transform.localPosition, transform.rotation) as GameObject;
                    GameObject MeteoroClone2 = Instantiate(meteoro, transform.localPosition, transform.rotation) as GameObject;
                    if (this.rb2d.velocity.x > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);

                    }
                    else if (this.rb2d.velocity.x < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -this.rb2d.velocity.x / 2, 0), ForceMode2D.Impulse);
                    }
                    else if (this.rb2d.velocity.y > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(-this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                    }
                    else if (this.rb2d.velocity.y < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody2D>().AddForce(new Vector3(this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody2D>().AddForce(new Vector3(-this.rb2d.velocity.y / 2, 0, 0), ForceMode2D.Impulse);
                    }
                    MeteoroClone1.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone1.GetComponent<MeteoroFaseB2>().escolheTamanho = 1;
                    MeteoroClone2.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone2.GetComponent<MeteoroFaseB2>().escolheTamanho = 1;
                    Instantiate(particle, transform.position, transform.rotation);
                    SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB2>().destroyMeteoro);
                }
                gameManager.GetComponent<GameManagerFaseB2>().score += 0.01f;
                Destroy(this.gameObject);
                Instantiate(particle, transform.position, transform.rotation);
                SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB2>().destroyMeteoro);
                if (!collision.gameObject.CompareTag("Player"))
                {
                    Destroy(collision.gameObject);
                }
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
