using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFaseZ : MonoBehaviour
{
    private GameObject textos;
    private LevelFlowManager levelFlowManager;
    private Rigidbody2D rb2D;
    private float vel = 100.0f, espera = 0f;

    private bool escolheu = false;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        textos = GameObject.Find("Canvas/TextoChoose");
        levelFlowManager = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();
    }

    void Update()
    {
        espera += Time.deltaTime;
        if (espera >= 2f && !escolheu)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb2D.AddForce(Vector2.up * vel * 2);
                textos.GetComponent<TextMeshProUGUI>().text = "What if...";
                escolheu = true;         
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb2D.AddForce(Vector2.right * vel);
                textos.GetComponent<TextMeshProUGUI>().text = "Maybe...";
                escolheu = true;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb2D.AddForce(Vector2.left * vel);
                textos.GetComponent<TextMeshProUGUI>().text = "Perhaps...";
                escolheu = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Cima")
        {
            levelFlowManager.nextLevel = "FaseB0";
            levelFlowManager.changeLevel("BetweenScenes");
        }
        if (collision.gameObject.name == "Esquerda")
        {
            levelFlowManager.nextLevel = "FaseA0"; ;
            levelFlowManager.changeLevel("BetweenScenes");
        }
        if (collision.gameObject.name == "Direita")
        {
            levelFlowManager.nextLevel = "FaseC0";
            levelFlowManager.changeLevel("BetweenScenes");
        }
    }
}
