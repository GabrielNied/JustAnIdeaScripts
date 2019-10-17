using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFaseB3 : MonoBehaviour
{

    private GameObject gameManager, canvas, player;
    public GameObject tiroPrefab, particle;

    public int escolheMov;

    private float radius = 375f, tiroCD = 0f, angle;
    private Vector2 centre;

    private MeshRenderer meshRenderer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
        centre = transform.localPosition;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        tiroCD += Time.deltaTime;

        Movimentacao();

        if (tiroCD >= 4f && GetComponent<Renderer>().isVisible)
        {
            Atira();
            tiroCD = Random.Range(-1, 0.5f);
        }

        if (gameManager.GetComponent<GameManagerFaseB3>().vida <= 0)
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
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
            angle += Time.deltaTime / 5;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle) * 2) * radius;
            transform.localPosition = centre + offset;
        }
        else if (escolheMov == 2)
        {
            angle += Time.deltaTime / 5;

            Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle) * 2) * radius;
            transform.localPosition = centre + offset * -1;
        }
        else if (escolheMov == 3)
        {
            angle += Time.deltaTime / 5;

            Vector2 offset = new Vector2(Mathf.Sin(angle) * 2.5f, Mathf.Cos(angle)) * radius;
            transform.localPosition = centre + offset;
        }
        else if (escolheMov == 4)
        {
            angle += Time.deltaTime / 5;

            Vector2 offset = new Vector2(Mathf.Sin(angle) * 2.5f, Mathf.Cos(angle)) * radius;
            transform.localPosition = centre + offset * -1;
        }
    }

    void Atira()
    {
        GameObject TiroClone = Instantiate(tiroPrefab, transform.localPosition, transform.rotation) as GameObject;
        TiroClone.GetComponent<Rigidbody>().AddForce(transform.up * 1000);
        TiroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (meshRenderer.isVisible)
        {
            if (collision.gameObject.tag == "TiroPlayer" || collision.gameObject.tag == "Player")
            {
                if (collision.gameObject.CompareTag("TiroPlayer"))
                {
                    gameManager.GetComponent<GameManagerFaseB3>().score += 3f;
                    Instantiate(particle, transform.position, transform.rotation);
                }
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
