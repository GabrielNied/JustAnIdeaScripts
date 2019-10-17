using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BetweenScenes : MonoBehaviour {

    public TextMeshProUGUI historyText;
    private LevelFlowManager levelFlowManager;
    private float tempoTransicao = 0f, tempoTutorial = 0f;

    private bool tutorial = false;

    public GameObject tutorialAtiva;

    void Start () {
        levelFlowManager = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();
        tutorialAtiva = GameObject.Find("Canvas/"+ levelFlowManager.nextLevel);
    }
	
	void Update () {
        if (tempoTransicao >= 2f && !tutorial)
        {
            historyText.GetComponent<Pisca>().smoothFade = true;
        }

        if (tempoTransicao >= 3f && !tutorial)
        {
            tutorial = true;
            foreach (Transform kid in tutorialAtiva.transform)
            {
                kid.gameObject.SetActive(true);
                kid.GetComponent<Pisca>().resetBlinking();
            }

        }
        else{
            tempoTransicao += Time.deltaTime;
        }

        if (tutorial && tempoTutorial < 3.25f)
        {
            tempoTutorial += Time.deltaTime;
        }
        else if(tutorial && tempoTutorial >= 3.25f)
        {
            levelFlowManager.changeLevel(levelFlowManager.nextLevel);
        }
	}
}
