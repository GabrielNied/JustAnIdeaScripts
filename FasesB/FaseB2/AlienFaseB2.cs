using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFaseB2 : MonoBehaviour {

    private GameObject gameManager, canvas, player;
    public GameObject tiroPrefab, particle;

    public int escolheMov;

    private float rotateSpeed = 0.1f, radius = 5f, tiroCD = 0f;
    private Vector2 centre;
    private float angle;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
        centre = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        tiroCD += Time.deltaTime;

        Movimentacao();

        if (tiroCD >= 3f && GetComponent<Renderer>().isVisible)
        {
            Atira();
            tiroCD = Random.Range(-1, 0.5f);
        }

        if (gameManager.GetComponent<GameManagerFaseB2>().vida <= 0 || gameManager.GetComponent<GameManagerFaseB2>().score >= 1)
        {
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void Movimentacao()
    {
        Vector3 alvo = (player.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, alvo);

        /*
        float errou = Random.Range(-10, 10f);
        Vector3 erraMira = new Vector3(player.transform.position.x + errou, player.transform.position.y + errou, player.transform.position.z);
        Vector3 alvo = (erraMira - transform.position);

        Quaternion lookRotation = Quaternion.LookRotation(alvo);

        float step = 1 * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(player.transform.rotation, lookRotation, step);
        */

        if (escolheMov == 1)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle) * 2) * radius;
            transform.position = centre + offset;
        }
        else if (escolheMov == 2)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle) * 2) * radius;
            transform.position = centre + offset * -1;
        }
        else if (escolheMov == 3)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle) * 3.25f, Mathf.Cos(angle)) * 4;
            transform.position = centre + offset;
        }
        else if (escolheMov == 4)
        {
            angle += rotateSpeed * Time.deltaTime;

            Vector2 offset = new Vector2(Mathf.Sin(angle) * 3.25f, Mathf.Cos(angle)) * 4;
            transform.position = centre + offset * -1;
        }
    }

    void Atira()
    {
        GameObject TiroClone = Instantiate(tiroPrefab, transform.localPosition, transform.rotation) as GameObject;
        TiroClone.GetComponent<Rigidbody2D>().AddForce(transform.up * 125);
        TiroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spriteRenderer.isVisible)
        {
            if (collision.gameObject.tag == "TiroPlayer" || collision.gameObject.tag == "Player")
            {
                gameManager.GetComponent<GameManagerFaseB2>().score += 0.05f;
                Instantiate(particle, transform.position, transform.rotation);
                Destroy(this.gameObject);
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
