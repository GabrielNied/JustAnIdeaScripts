using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC2 : MonoBehaviour {

    public Transform[] positions;
    public int posicao, opcoes = 3;
    private CircleCollider2D colPlay;
    public Color playColor, opacityColor;
    public bool piscando = false;
    public float tempo;
    private GameObject gameManager;

    void Start () {
        colPlay = GetComponent<CircleCollider2D>();
        playColor = GameObject.Find("PlayerSpriteC1").GetComponent<SpriteRenderer>().color;
        opacityColor = Color.white;
        opacityColor.a = -4;
        gameManager = GameObject.Find("Canvas");
    }
	
	
	void Update () {
        
        Move();
        if (UIManagerC0.InstanciaUI.Morreu)
        {
            SoundManager.instance.musicSource.Stop();
            GameManagerC2.InstanceGMC2.lfw.gameLost();
            Destroy(gameObject);
        }
        
        if (piscando)
        {
            Color lerpedColor = Color.Lerp(playColor, opacityColor, Mathf.PingPong(tempo, 0.25f));
            GameObject.Find("PlayerSpriteC1").GetComponent<SpriteRenderer>().color = lerpedColor;

            if(tempo >= 2)
            {
                colPlay.enabled = true;
                piscando = false;
                GameObject.Find("PlayerSpriteC1").GetComponent<SpriteRenderer>().color = playColor;
            }

            
        }
        tempo += Time.deltaTime;

    }

    public void Move()
    {
        Teleportar();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (posicao < opcoes)
            {
                posicao++;
                Teleportar();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (posicao > 1)
            {
                posicao--;
                Teleportar();
            }

        }
    }

    public void Teleportar()
    {
        if(posicao == 1)
        {
            transform.position = positions[0].transform.position;
        }
        else if (posicao == 2)
        {
            transform.position = positions[1].transform.position;
        }
        else if (posicao == 3)
        {
            transform.position = positions[2].transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Enemy")
        {
            SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerC2>().vagalume);
            UIManagerC0.InstanciaUI.GetDamage();
            posicao = 2;
            colPlay.enabled = false;
            piscando = true;
            tempo = 0;
        }
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Cogumelo")
        {
            SoundManager.instance.RandomizeSfx(gameManager.GetComponent<GameManagerC2>().cogumelo);
            Destroy(col.gameObject);
            GameManagerC2.InstanceGMC2.cogumelosParaVencer++;
        }
    }
}
