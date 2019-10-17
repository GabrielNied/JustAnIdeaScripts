using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerFaseB1 : MonoBehaviour
{
    private LevelFlowManager levelFlowManager;
    private GameObject spawner, barFill;
    public int vida = 3;
    private int ultimaVida = 3;
    public float score = 0;
    public List<GameObject> vidasUI;
    public GameObject vidaUI, player;

    public AudioClip trilha, comendoUranio, destroyPlayer;

    void Start()
    {
        vidaUI = GameObject.Find("Canvas/Vida");
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = GameObject.Find("Canvas/Spawner");
        barFill = GameObject.Find("Canvas/PlayerFaseB1/ImageFill");
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
        barFill.GetComponent<Image>().fillAmount = score;

        //Vitoria
        if (barFill.GetComponent<Image>().fillAmount == 1 || Input.GetKeyDown(KeyCode.Delete))
        {
            spawner.GetComponent<SpawnerFaseB1>().podeSpawnarNormal = false;
            player.SetActive(false);
            levelFlowManager.gameWon(LevelNumbers.B1);
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
            SoundManager.instance.musicSource.Stop();
            spawner.GetComponent<SpawnerFaseB1>().podeSpawnarNormal = false;
            player.SetActive(false);
            levelFlowManager.gameLost();
        }
    }
}
