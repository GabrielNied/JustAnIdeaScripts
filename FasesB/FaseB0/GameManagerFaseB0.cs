using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerFaseB0 : MonoBehaviour
{
    private LevelFlowManager levelFlowManager;

    private GameObject textScore, textScoreLess, spawner;
    public int vida = 3, score = 100;
    private int ultimaVida = 3, ultimoScore = 100;
    public List<GameObject> vidasUI;
    public GameObject vidaUI, player;

    void Start()
    {
        levelFlowManager = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();
        spawner = GameObject.Find("Canvas/Spawner");
        textScore = GameObject.Find("Canvas/Score");
        textScoreLess = GameObject.Find("Canvas/ScoreLess");
        vidaUI = GameObject.Find("Canvas/Vida");
        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < vidaUI.GetComponent<Transform>().childCount; i++)
        {
            GameObject filhoVida = vidaUI.transform.GetChild(i).gameObject;
            vidasUI.Add(filhoVida);
        }
    }

    void Update()
    {
        if (ultimoScore > score || ultimoScore < score)
        {
            textScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
            ultimoScore = score;
        }

        //Vitoria
        if (score <= 0 || Input.GetKeyDown(KeyCode.Delete))
        {
            spawner.GetComponent<SpawnerFaseB0>().podeSpawnarNormal = false;
            player.SetActive(false);
            levelFlowManager.gameWon(LevelNumbers.B0);
            SoundManager.instance.musicSource.Stop();
        }

        if (ultimaVida > vida)
        {
            vidasUI[0].gameObject.SetActive(false);
            vidasUI.RemoveAt(0);
            ultimaVida = vida;
        }

        //Derrota
        if (vida <= 0)
        {
            spawner.GetComponent<SpawnerFaseB0>().podeSpawnarNormal = false;
            player.SetActive(false);
            levelFlowManager.gameLost();
            SoundManager.instance.musicSource.Stop();
        }
    }

    public void getPoint()
    {
        score += 10;
        textScoreLess.GetComponent<Pisca>().resetBlinking();
    }

    public void lossPoint()
    {
        if (!player.GetComponent<PlayerFaseB0>().imortal)
        {
            score -= 1;
            textScore.GetComponent<Pisca>().resetBlinking();
        }
    }

    public void getDamage()
    {
        vida -= 1;
        vidaUI.GetComponent<Pisca>().resetBlinkingVisible();
    }
}
