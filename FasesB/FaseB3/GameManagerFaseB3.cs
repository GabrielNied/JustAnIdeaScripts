using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerFaseB3 : MonoBehaviour
{
    private LevelFlowManager levelFlowManager;
    private GameObject scoreText, spawner;
    public int vida = 3;
    public float score = 0;
    private int ultimaVida = 3;
    public List<GameObject> vidasUI;
    public GameObject vidaUI, player;

    public AudioClip trilha, destroyPlayer, destroyMeteoro, tiro, comendoUranio, propulsor;

    void Start()
    {
        vidaUI = GameObject.Find("Canvas/Vida");
        spawner = GameObject.Find("Canvas/Spawner");
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GameObject.Find("Canvas/ScoreText");
        levelFlowManager = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();

        for (int i = 0; i < vidaUI.GetComponent<Transform>().childCount; i++)
        {
            GameObject filhoVida = vidaUI.transform.GetChild(i).gameObject;
            vidasUI.Add(filhoVida);
        }

        SoundManager.instance.musicSource.clip = trilha;
        SoundManager.instance.musicSource.Play();
    }

    void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();

        //Vitoria
        if (score >= 100 || Input.GetKeyDown(KeyCode.Delete))
        {
            spawner.GetComponent<SpawnerFaseB3>().podeSpawnarNormal = false;
            player.SetActive(false);
            levelFlowManager.gameWon(LevelNumbers.B3);
            SoundManager.instance.musicSource.Stop();
        }

        if (ultimaVida > vida)
        {
            vidaUI.GetComponent<Pisca>().resetBlinkingVisible();
            vidasUI[0].gameObject.SetActive(false);
            vidasUI.RemoveAt(0);
            ultimaVida = vida;
        }

        //Derrota
        if (vida <= 0)
        {
            spawner.GetComponent<SpawnerFaseB3>().podeSpawnarNormal = false;
            player.SetActive(false);
            levelFlowManager.gameLost();
            SoundManager.instance.musicSource.Stop();
        }
    }
}
