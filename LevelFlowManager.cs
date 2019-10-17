using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class LevelFlowManager : MonoBehaviour {

    public float fadeTime = 0.75f;

    private static LevelFlowManager instance = null;
    private AsyncOperation asyncLoader;
    private bool doingFadeOut, doingFadeIn, changingLevel = false;
    private RawImage _image;
    private GameObject coverUp;

    private List<LevelNumbers> beatenLevels;

    public string nextLevel;

    private void Awake()
    {
        if (instance == null) instance = this; else Destroy(this.gameObject);
        beatenLevels = new List<LevelNumbers>();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.activeSceneChanged += ReadyNewScene;
    }
    
    private void Update()
    {
        if (doingFadeOut || doingFadeIn) return;
        if (SceneManager.GetActiveScene().name == "LoadBeforeMenu")
            this.changeLevel("Menu");
    }
    
    private void ReadyNewScene (Scene current, Scene next)
    {
        coverUp = GameObject.Find("CoverUp");
        if (!coverUp)
        {
            coverUp = new GameObject("CoverUp", typeof(RawImage));
            coverUp.transform.SetParent(GameObject.Find("Canvas").transform);
            coverUp.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
            coverUp.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
            coverUp.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            coverUp.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            coverUp.transform.position = new Vector3(0f, 0f, 0f);
        }

        _image = coverUp.GetComponent<RawImage>();
        _image.color = Color.black;
        _image.raycastTarget = false;

        this.FadeIn();
        changingLevel = false;
    }

    private void FadeIn()
    {
        doingFadeIn = true;
        StartCoroutine(Fade(_image, fadeTime, Color.black, Color.clear, true));
    }

    private void FadeOut(string levelName)
    {
        doingFadeOut = true;
        StartCoroutine(Fade(_image, fadeTime, Color.clear, Color.black, false, levelName));
    }

    IEnumerator Fade(RawImage mat, float duration, Color startColor, Color endColor, bool fadingIn, string levelName = "")
    {
        if (!fadingIn)
        {
            asyncLoader = SceneManager.LoadSceneAsync(levelName);
            asyncLoader.allowSceneActivation = false;
        }

        if (SceneManager.GetActiveScene().name == "LoadBeforeMenu")
            duration = 0.05f;

        float start = Time.time;
        float elapsed = 0;
        while (elapsed < duration)
        {
            // calculate how far through we are
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            mat.color = Color.Lerp(startColor, endColor, normalisedTime);
            // wait for the next frame
            yield return null;
        }

        if (fadingIn) doingFadeIn = false;
        else
        {
            asyncLoader.allowSceneActivation = true;
            doingFadeOut = false;
        }
    }

    public List<LevelNumbers> getBeatenLevels()
    {
        return beatenLevels;
    }

    public void gameWon (LevelNumbers level)
    {
        if (!beatenLevels.Contains(level)) beatenLevels.Add(level);

        if ((level == LevelNumbers.A3 ||
            level == LevelNumbers.B3 ||
            level == LevelNumbers.C3) && 
            (beatenLevels.Contains(LevelNumbers.A3) &&
            beatenLevels.Contains(LevelNumbers.B3) &&
            beatenLevels.Contains(LevelNumbers.C3))
            )
        {
            this.changeLevel("Creditos");
        } else
        {
            this.changeLevel("LevelSelectionScreen");
        }
    }

    public void gameLost()
    {
        this.changeLevel("LevelSelectionScreen");
    }

    public void changeLevel(string levelName)
    {
        if (changingLevel || doingFadeIn || doingFadeOut) return;

        this.FadeOut(levelName);
        changingLevel = true;
    }

    public bool fadeInDone()
    {
        return !doingFadeIn;
    }
}
