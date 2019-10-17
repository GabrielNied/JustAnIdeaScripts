using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

    public Color disabledColor, enabledColor, beatenColor;
    public Sprite pinkB, purpleB, greenB;
    
    private LevelFlowManager levelFlowManager;
    private List<LevelNumbers> availableLevels, beatenLevels;

    public AudioClip trilha;

    // Use this for initialization
    void Start () {
        if (!SoundManager.instance.musicSource.isPlaying)
        {
            SoundManager.instance.musicSource.clip = trilha;
            SoundManager.instance.musicSource.Play();
        }

        levelFlowManager = GameObject.FindWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();
        beatenLevels = levelFlowManager.getBeatenLevels();

        availableLevels = new List<LevelNumbers>();
        availableLevels.Add(LevelNumbers.A0);
        availableLevels.Add(LevelNumbers.B0);
        availableLevels.Add(LevelNumbers.C0);

        if (beatenLevels.Contains(LevelNumbers.A0))
        {
            availableLevels.Add(LevelNumbers.A1);
            availableLevels.Add(LevelNumbers.A2);
        }

        if (beatenLevels.Contains(LevelNumbers.B0))
        {
            availableLevels.Add(LevelNumbers.B1);
            availableLevels.Add(LevelNumbers.B2);
        }

        if (beatenLevels.Contains(LevelNumbers.C0))
        {
            availableLevels.Add(LevelNumbers.C1);
            availableLevels.Add(LevelNumbers.C2);
        }
        
        if (beatenLevels.Contains(LevelNumbers.A1) && beatenLevels.Contains(LevelNumbers.A2)) availableLevels.Add(LevelNumbers.A3);
        if (beatenLevels.Contains(LevelNumbers.B1) && beatenLevels.Contains(LevelNumbers.B2)) availableLevels.Add(LevelNumbers.B3);
        if (beatenLevels.Contains(LevelNumbers.C1) && beatenLevels.Contains(LevelNumbers.C2)) availableLevels.Add(LevelNumbers.C3);

        foreach (LevelNumbers level in availableLevels)
        {
            string levelName = level.ToString();
            GameObject.Find(levelName).GetComponent<Animator>().enabled = true;

            if (levelName[1].ToString() != "3") GameObject.Find("c" + levelName).GetComponent<SpriteRenderer>().enabled = true;
        }

        foreach (LevelNumbers level in beatenLevels)
        {
            string levelName = level.ToString();
            
            GameObject.Find(levelName).GetComponent<Animator>().enabled = false;
            Image levelImage = GameObject.Find(level.ToString()).GetComponent<Image>();
            levelImage.color = beatenColor;

            if (levelName[1].ToString() == "1" || levelName[1].ToString() == "2")
                GameObject.Find(levelName[1].ToString() + levelName[0].ToString() + "3").GetComponent<SpriteRenderer>().enabled = true;

            switch (levelName[0].ToString())
            {
                case "A":
                    levelImage.sprite = pinkB;
                    break;
                case "B":
                    levelImage.sprite = purpleB;
                    break;
                case "C":
                    levelImage.sprite = greenB;
                    break;
                default:
                    break;
            }
        }
    }
    
    public void loadStage(string levelName)
    {
        foreach (LevelNumbers level in availableLevels)
        {
            if ("Fase" + level.ToString() == levelName)
                levelFlowManager.nextLevel = levelName;
                levelFlowManager.changeLevel("BetweenScenes");
        }
    }
}
