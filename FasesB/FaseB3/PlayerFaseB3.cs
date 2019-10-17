using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFaseB3 : MonoBehaviour
{

    private Rigidbody rb;

    private MeshRenderer meshRenderer;
    private float tempoPisca = 0f, tempoImortal = 0f, rotacao = 5f, impulso = 30f, tempoAtira = 0.15f, maxvel = 1000, energiaTiro = 0f, maxVolume, volumeInicial;
    private bool imortal = false, podeAndar = true, podeAtirar = true, isWrappingX = false, isWrappingY = false, ativo = true;
    private Vector2 posInicial;
    private GameObject gameManager, canvas, barFill, propulsor;
    public GameObject tiroPrefab, particleComendoUranio, particleDano;
    public List<GameObject> piscarJuntoPlayer;

    void Start()
    {
        volumeInicial = SoundManager.instance.propulsorSource.volume;
        maxVolume = SoundManager.instance.propulsorSource.volume;
        propulsor = GameObject.Find("Canvas/PlayerFaseB3/Nave/Propulsor");
        canvas = GameObject.Find("Canvas");
        gameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        posInicial = new Vector2(transform.localPosition.x, transform.localPosition.y);
        barFill = GameObject.Find("Canvas/PlayerFaseB3/Nave/ImageFill");

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject filhoPlayer = transform.GetChild(i).gameObject;
            piscarJuntoPlayer.Add(filhoPlayer);
        }
    }

    private void Update()
    {
        tempoAtira += Time.deltaTime;
        tempoImortal += Time.deltaTime;
        tempoPisca += Time.deltaTime;

        barFill.GetComponent<Image>().fillAmount = energiaTiro;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (podeAtirar && tempoAtira >= 0.25f && energiaTiro > 0.30f)
            {
                Atira();
                tempoAtira = 0;
                energiaTiro -= 0.33f;
            }
        }

        //Dano
        if (imortal && tempoImortal >= 1.5f)
        {
            podeAndar = true;
            podeAtirar = true;
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
                meshRenderer.enabled = true;
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
            rb.angularVelocity = new Vector3(0, 0, -moveHorizontal) * rotacao;

            if (rb.velocity.sqrMagnitude < maxvel)
            {
                float moveVertical = Input.GetAxis("VerticalFaseB2");
                rb.AddForce(transform.up * moveVertical * impulso);
            }

            //Forca Contraria
            Vector2 vMovement = rb.velocity.normalized;
            Vector2 vFacing = transform.forward;
            Vector2 diff = vFacing - vMovement;
            rb.AddForce(diff * impulso / 4);

            if (Input.GetAxis("VerticalFaseB2") != 0)
            {
                propulsor.SetActive(true);

                if (!SoundManager.instance.propulsorSource.isPlaying)
                {
                    SoundManager.instance.PlayPropulsor(gameManager.GetComponent<GameManagerFaseB3>().propulsor);
                }

                if (SoundManager.instance.propulsorSource.volume <= maxVolume && volumeInicial > 0)
                {
                    SoundManager.instance.propulsorSource.volume += Time.deltaTime * 2;
                }
            }
            else
            {
                propulsor.SetActive(false);
                SoundManager.instance.propulsorSource.volume -= Time.deltaTime * 3;
            }
        }
    }

    void Atira()
    {
        GameObject TiroClone = Instantiate(tiroPrefab, transform.localPosition, transform.rotation) as GameObject;
        TiroClone.GetComponent<Rigidbody>().AddForce(transform.up * 10000);
        TiroClone.transform.SetParent(canvas.GetComponent<Transform>(), false);

        SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB3>().tiro);
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
        if (meshRenderer.isVisible)
        {
            return true;
        }
        return false;
    }

    void Pisca()
    {
        if (tempoPisca >= 0.1f)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            ativo = !ativo;
            foreach (GameObject filhosPlayer in piscarJuntoPlayer)
            {
                filhosPlayer.SetActive(ativo);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (meshRenderer.isVisible)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "TiroEnemy" || collision.gameObject.tag == "Meteoro")
            {
                if (!imortal)
                {
                    Instantiate(particleDano, transform.position, transform.rotation);
                    SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB3>().destroyPlayer);
                    SoundManager.instance.propulsorSource.volume = 0;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = new Vector3(0, 0, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.localPosition = posInicial;
                    energiaTiro = 0;
                    podeAndar = false;
                    podeAtirar = false;
                    propulsor.SetActive(false);
                    gameManager.GetComponent<GameManagerFaseB3>().vida -= 1;
                }
                Imortal();
            }

            if (collision.gameObject.tag == "Uranio")
            {
                Instantiate(particleComendoUranio, transform.position, transform.rotation);
                SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerFaseB3>().comendoUranio);
                energiaTiro = 1f;
                Destroy(collision.gameObject);
            }
        }
    }
}