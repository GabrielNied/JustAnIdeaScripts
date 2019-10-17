using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC1 : MonoBehaviour
{
    public static PlayerC1 InstancePlayerC1;

    private Rigidbody2D playerRb;
    [SerializeField]
    private float _speed = 100f, _jumpForce = 400f;

    [SerializeField]
    private bool _isGround, saiuDoChao, podeInvi = true;

    public float tempoCol;

    private Vector2 posicaoInicial1;
    public SpriteRenderer playerRenderer;
    public GameObject imagemPreta, particulaFumaca1, particulaAviso, trailEscuro, trailClaro, particulaFumaca2;
    public List<GameObject> sapos;
    private GameObject sapoPai;

    void Start()
    {
        InstancePlayerC1 = this;
        playerRenderer = GameObject.Find("PlayerSpriteC1").GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        posicaoInicial1 = new Vector2(-6.2f, -2.8f);
        particulaAviso.SetActive(false);
        trailEscuro.SetActive(false);
        trailClaro.SetActive(true);
        sapoPai = GameObject.Find("Canvas/Sapos");

        for (int i = 0; i < sapoPai.GetComponent<Transform>().childCount; i++)
        {
            GameObject filhoSapo = sapoPai.transform.GetChild(i).gameObject;
            sapos.Add(filhoSapo);
        }



    }


    void Update()
    {
        Movement();
        Jump();
        CheckDeath();
        tempoCol += Time.deltaTime;
        Invisivel();

        if (tempoCol >= 0.1 && saiuDoChao)
        {
            _isGround = false;
        }
       

    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(moveHorizontal * _speed, playerRb.velocity.y);
    }

    private void Jump()
    {
        if (_isGround)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerRb.AddForce(Vector2.up * _jumpForce);
                saiuDoChao = true;
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "PlataformaC0")
        {
            tempoCol = 0;
            _isGround = true;
            saiuDoChao = false;
        }

        if(col.tag == "Cogumelo")
        {
            Destroy(col.gameObject);
            GameManagerC1.gmC1Ref.cogumelosParaVencer++;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "PlataformaC0")
        {
            tempoCol = 0;
            _isGround = true;
            saiuDoChao = false;
        }
    }

        private void CheckDeath()
    {
        if (UIManagerC0.InstanciaUI.Morreu == true)
        {
            SoundManager.instance.musicSource.Stop();
            GameManagerC1.gmC1Ref.lfw.gameLost();
            Destroy(gameObject);
            
        }
        else
        {
            if (transform.position.y <= -5.5f)
            {
                transform.position = posicaoInicial1;
                UIManagerC0.InstanciaUI.GetDamage();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "SapoFaseC1")
        {
            UIManagerC0.InstanciaUI.GetDamage();
            transform.position = posicaoInicial1;
        }
    }

    public void Invisivel()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (podeInvi && GameManagerC1.gmC1Ref.podeInvBar)
            {
                imagemPreta.SetActive(true);
                particulaAviso.SetActive(true);
                trailEscuro.SetActive(true);
                trailClaro.SetActive(false);



                // Color inv = new Color(255, 0, 163);
                Color inv = new Color(255, 0, 59, 0.60f);
                
                playerRenderer.color = inv;
                podeInvi = false;
                
                foreach(GameObject sapinhos in sapos)
                {
                    Physics2D.IgnoreCollision(sapinhos.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
                }

                GameManagerC1.gmC1Ref.invisivel = true;

                GameObject particleAux1 = Instantiate(particulaFumaca1, transform.position,Quaternion.identity);
                particleAux1.transform.position = transform.position;
                Destroy(particleAux1, 2f);

                PiscaC1.InstancePiscaC1.tempo = 0;


            }
            else if(GameManagerC1.gmC1Ref.auxBol)
            {
                GameObject particleAux2 = Instantiate(particulaFumaca2, transform.position, Quaternion.identity);
                particleAux2.transform.position = transform.position;
                Destroy(particleAux2, 2f);

                trailEscuro.SetActive(false);
                trailClaro.SetActive(true);
                particulaAviso.SetActive(false);
                imagemPreta.SetActive(false);
                playerRenderer.color = Color.white;
                podeInvi = true;
                
                foreach (GameObject sapinhos in sapos)
                {
                    Physics2D.IgnoreCollision(sapinhos.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }

                GameManagerC1.gmC1Ref.invisivel = false;
                
            }
            
           
        }
    }

    
}
