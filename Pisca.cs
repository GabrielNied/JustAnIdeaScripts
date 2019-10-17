using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pisca : MonoBehaviour {

    public bool blinking = true, smoothFade = true, timerBased = false, endVisible = false;
    public float blinkForSeconds = 5f, blinkSpeed = 1f, delay = 0f;

    public enum BlinkTypes { TextMeshPro, TextMeshProVisivel, TextMeshProFadeOut, TextMeshProFadeIn, Sprite, SpriteFadeOut, SpriteFadeIn, GameObject, GameObjectFilhos};
    public BlinkTypes blinkType = BlinkTypes.TextMeshPro;

    private float innerTimer = 0f, delayTimer = 0f, fakeTimeForPP = 0f;

    private void Start()
    {

    }

    private void Update()
    {
        if (delayTimer < delay) {
            delayTimer += Time.deltaTime;
            return;
        } 

        switch (blinkType)
        {
            case BlinkTypes.TextMeshPro:
                if (blinking || (smoothFade && gameObject.GetComponent<TextMeshProUGUI>().color.a >= 0.01f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<TextMeshProUGUI>().color = new Color(
                        gameObject.GetComponent<TextMeshProUGUI>().color.r, 
                        gameObject.GetComponent<TextMeshProUGUI>().color.g,
                        gameObject.GetComponent<TextMeshProUGUI>().color.b, 
                        Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f)
                        );
                }
                break;

            case BlinkTypes.TextMeshProVisivel:
                if (blinking || (smoothFade && gameObject.GetComponent<TextMeshProUGUI>().color.a <= 0.9f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<TextMeshProUGUI>().color = new Color(
                        gameObject.GetComponent<TextMeshProUGUI>().color.r,
                        gameObject.GetComponent<TextMeshProUGUI>().color.g,
                        gameObject.GetComponent<TextMeshProUGUI>().color.b,
                        Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f)
                        );
                }
                break;

            case BlinkTypes.GameObject:
                if (blinking || (smoothFade && gameObject.transform.localScale.x >= 0.01f) || (endVisible && gameObject.transform.localScale.x >= 1f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    Vector3 mudaTamanho = new Vector3(Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f), Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f), Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f));
                    gameObject.transform.localScale = mudaTamanho;
                }
                break;

            case BlinkTypes.GameObjectFilhos:
                fakeTimeForPP += Time.deltaTime;
                foreach (Transform kid in gameObject.transform)
                {
                    if (blinking || (smoothFade && kid.localScale.x >= 0.01f) || (endVisible && kid.localScale.x >= 1f))
                    {
                        Vector3 mudaTamanho = new Vector3(Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f), Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f), Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f));
                        kid.localScale = mudaTamanho;
                    }
                }
                break;

            case BlinkTypes.Sprite:
                if (blinking || (smoothFade && gameObject.GetComponent<SpriteRenderer>().color.a >= 0.01f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(
                        gameObject.GetComponent<SpriteRenderer>().color.r,
                        gameObject.GetComponent<SpriteRenderer>().color.g,
                        gameObject.GetComponent<SpriteRenderer>().color.b, 
                        Mathf.PingPong(fakeTimeForPP * blinkSpeed, 1.0f)
                        );
                }
                break;

            case BlinkTypes.SpriteFadeOut:
                if (blinking || (smoothFade && gameObject.GetComponent<SpriteRenderer>().color.a >= 0.01f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(
                        gameObject.GetComponent<SpriteRenderer>().color.r,
                        gameObject.GetComponent<SpriteRenderer>().color.g,
                        gameObject.GetComponent<SpriteRenderer>().color.b,
                        Mathf.SmoothStep(1, 0, fakeTimeForPP * blinkSpeed)
                        );
                }
                break;

            case BlinkTypes.SpriteFadeIn:
                if (blinking || (smoothFade && gameObject.GetComponent<SpriteRenderer>().color.a < 1f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(
                        gameObject.GetComponent<SpriteRenderer>().color.r,
                        gameObject.GetComponent<SpriteRenderer>().color.g,
                        gameObject.GetComponent<SpriteRenderer>().color.b,
                        Mathf.SmoothStep(0, 1, fakeTimeForPP * blinkSpeed)
                        );
                }
                break;

            case BlinkTypes.TextMeshProFadeOut:
                if (blinking || (smoothFade && gameObject.GetComponent<TextMeshProUGUI>().color.a >= 0.01f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<TextMeshProUGUI>().color = new Color(
                        gameObject.GetComponent<TextMeshProUGUI>().color.r,
                        gameObject.GetComponent<TextMeshProUGUI>().color.g,
                        gameObject.GetComponent<TextMeshProUGUI>().color.b,
                        Mathf.SmoothStep(1, 0, fakeTimeForPP * blinkSpeed)
                        );
                }
                break;

            case BlinkTypes.TextMeshProFadeIn:
                if (blinking || (smoothFade && gameObject.GetComponent<TextMeshProUGUI>().color.a < 1f))
                {
                    fakeTimeForPP += Time.deltaTime;
                    gameObject.GetComponent<TextMeshProUGUI>().color = new Color(
                        gameObject.GetComponent<TextMeshProUGUI>().color.r,
                        gameObject.GetComponent<TextMeshProUGUI>().color.g,
                        gameObject.GetComponent<TextMeshProUGUI>().color.b,
                        Mathf.SmoothStep(0, 1, fakeTimeForPP * blinkSpeed)
                        );
                }
                break;

            default:
                break;
        }

        if (timerBased)
        {
            innerTimer += Time.deltaTime;
            if (innerTimer >= blinkForSeconds)
            {
                blinking = false;
            }
        }
    }

    public void resetBlinking()
    {
        blinking = true;
        innerTimer = 0f;
        delayTimer = 0f;
        fakeTimeForPP = 0f;
    }

    public void resetBlinkingVisible()
    {
        blinking = true;
        innerTimer = 0f;
        delayTimer = 0f;
        fakeTimeForPP = 0f;

        endVisible = true;
    }
}
