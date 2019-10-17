using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroFaseB3 : MonoBehaviour
{

    private GameObject gameManager, player, canvas;
    private float mov = 1000f;

    public int escolheMov = 0, escolheTamanho = 0;

    private Rigidbody rb;

    private bool andou = false;

    public GameObject meteoroP, meteoroM;

    private Vector2 screenPosition;
    private Vector3 rotacaoInicial;

    private MeshRenderer meshRenderer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
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
            mov = 1000f;
        }
        else if (escolheTamanho == 4 || escolheTamanho == 5)
        {
            mov = 750f;
        }
        else if (escolheTamanho == 6)
        {
            mov = 500f;
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

    private void OnTriggerEnter(Collider collision)
    {
        if (meshRenderer.isVisible)
        {
            if (collision.gameObject.tag == "TiroPlayer" || collision.gameObject.tag == "TiroEnemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Uranio")
            {
                if (escolheTamanho == 6)
                {
                    GameObject MeteoroClone1 = Instantiate(meteoroM, transform.localPosition, transform.rotation) as GameObject;
                    GameObject MeteoroClone2 = Instantiate(meteoroM, transform.localPosition, transform.rotation) as GameObject;
                    if (this.rb.velocity.x > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, this.rb.velocity.x / 2, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, -this.rb.velocity.x / 2, 0), ForceMode.Impulse);

                    }
                    else if (this.rb.velocity.x < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, this.rb.velocity.x / 2, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, -this.rb.velocity.x / 2, 0), ForceMode.Impulse);
                    }
                    else if (this.rb.velocity.y > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(-this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                    }
                    else if (this.rb.velocity.y < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(-this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                    }
                    MeteoroClone1.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone1.GetComponent<MeteoroFaseB3>().escolheTamanho = 4;
                    MeteoroClone2.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone2.GetComponent<MeteoroFaseB3>().escolheTamanho = 4;

                    if (collision.gameObject.CompareTag("TiroPlayer"))
                    {
                        gameManager.GetComponent<GameManagerFaseB3>().score += 3f;
                        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB3>().destroyMeteoro);
                    }
                    Destroy(this.gameObject);
                }
                else if (escolheTamanho == 4 || escolheTamanho == 5)
                {
                    GameObject MeteoroClone1 = Instantiate(meteoroP, transform.localPosition, transform.rotation) as GameObject;
                    GameObject MeteoroClone2 = Instantiate(meteoroP, transform.localPosition, transform.rotation) as GameObject;
                    if (this.rb.velocity.x > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, this.rb.velocity.x / 2, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, -this.rb.velocity.x / 2, 0), ForceMode.Impulse);

                    }
                    else if (this.rb.velocity.x < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, this.rb.velocity.x / 2, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.x, -this.rb.velocity.x / 2, 0), ForceMode.Impulse);
                    }
                    else if (this.rb.velocity.y > 0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(-this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                    }
                    else if (this.rb.velocity.y < -0.1f)
                    {
                        MeteoroClone1.GetComponent<Rigidbody>().AddForce(new Vector3(this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                        MeteoroClone2.GetComponent<Rigidbody>().AddForce(new Vector3(-this.rb.velocity.y / 2, this.rb.velocity.y, 0), ForceMode.Impulse);
                    }
                    MeteoroClone1.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone1.GetComponent<MeteoroFaseB3>().escolheTamanho = 1;
                    MeteoroClone2.transform.SetParent(canvas.GetComponent<Transform>(), false);
                    MeteoroClone2.GetComponent<MeteoroFaseB3>().escolheTamanho = 1;

                    if (collision.gameObject.CompareTag("TiroPlayer"))
                    {
                        gameManager.GetComponent<GameManagerFaseB3>().score += 2f;
                        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB3>().destroyMeteoro);
                    }
                    Destroy(this.gameObject);
                }
                else
                {
                    if (collision.gameObject.CompareTag("TiroPlayer"))
                    {
                        gameManager.GetComponent<GameManagerFaseB3>().score += 1f;
                        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB3>().destroyMeteoro);
                    }
                    Destroy(this.gameObject);
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
