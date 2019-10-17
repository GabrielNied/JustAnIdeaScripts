using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC0 : MonoBehaviour {

    private Rigidbody2D playerRb;
    [SerializeField]
    private float _speed = 100f, _jumpForce = 400f;

    [SerializeField]
    private bool _isGround, saiuDoChao;

    public float tempoCol; 

    private Vector2 posicaoInicial1;
    private LevelFlowManager lfw;

    void Start () {
        playerRb = GetComponent<Rigidbody2D>();
        posicaoInicial1 = new Vector2(-6.2f, -2.8f);
        lfw = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();

    }
	
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            lfw.gameWon(LevelNumbers.C0);
        }

        Movement();
        Jump();
        CheckDeath();
        tempoCol += Time.deltaTime;

        if(tempoCol >= 0.1 && saiuDoChao)
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
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerRb.AddForce(Vector2.up * _jumpForce );
                saiuDoChao = true;
            }
            
        }
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.transform.tag == "PlataformaC0")
    //    {
    //        tempoCol = 0;
    //        _isGround = true;
    //        saiuDoChao = false;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "PlataformaC0")
        {
            tempoCol = 0;
            _isGround = true;
            saiuDoChao = false;
        }

        if(col.name == "Final")
        {
            lfw.gameWon(LevelNumbers.C0);
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
            lfw.gameLost();
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
}
