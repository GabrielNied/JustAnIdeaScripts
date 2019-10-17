using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerA1 : MonoBehaviour {

    public float timerWaitUp = 1.5f, timerRelaunch = 2f, playTimeMax = 60, timeLostPerBall = 2f;
    public int startingMagnitude = 4;
    public GameObject ballPrefab;

    public AudioClip trilha, beep;

    private TextMeshProUGUI textTime, textPoints, textLevel;
    private GameObject feedbackPoints, feedbackTime, player;
    private float playTime = 0f, innerTimer = 0f, playTimeTotal, playTimeLeft, currentMagnitude;
    private int playPoints = 0, ballsMissed = 0;
    private bool gameStarted = false, ballfired = false, endGame = false;
    private Vector2 throwForce;

    private LevelFlowManager lfw;

    void Start () {
        textTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        textPoints = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        textLevel = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        feedbackTime = GameObject.Find("TimeLess");
        feedbackPoints = GameObject.Find("PointsPlus");
        player = GameObject.Find("Player");
        currentMagnitude = startingMagnitude;

        lfw = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();

        SoundManager.instance.musicSource.clip = trilha;
        SoundManager.instance.musicSource.Play();
    }
	
	void Update ()
    {
        if (!lfw.fadeInDone()) return;

        innerTimer += Time.deltaTime;

        playTimeLeft = playTimeMax - playTimeTotal - ballsMissed * timeLostPerBall;
        textTime.text = "Time Left: " + playTimeLeft.ToString("0") + "s";
        textPoints.text = "Points: " + playPoints.ToString();
        textLevel.text = "Speed " + (currentMagnitude).ToString();

        if (playTimeLeft < 0 || endGame || Input.GetKeyDown(KeyCode.Delete))
        {
            SoundManager.instance.musicSource.Stop();
            if (playPoints > 20) lfw.gameWon(LevelNumbers.A1);
            else lfw.gameLost();
        }

        if (!gameStarted && innerTimer >= timerWaitUp)
        {
            timerWaitUp = timerRelaunch;
            gameStarted = true;
        }
        else if (gameStarted)
        {
            playTimeTotal += Time.deltaTime;

            if (playTimeTotal >= playTimeMax * 3 / 4)
            {
                currentMagnitude = startingMagnitude * 1.7f;
            }
            else if (playTimeTotal >= playTimeMax * 2 / 4)
            {
                currentMagnitude = startingMagnitude * 1.4f;
            }
            else if (playTimeTotal >= playTimeMax * 1 / 4)
            {
                currentMagnitude = startingMagnitude * 1.2f;
            }

            // Game Looparoonie
            if (!ballfired)
            {
                // Define angle e position based on player
                // Instantiate a ball
                GameObject ball = Instantiate(
                    ballPrefab,
                    new Vector3(player.transform.position.x, player.transform.position.y * 0.9f, 0f),
                    Quaternion.identity,
                    this.transform);

                // Fire the ball 
                throwForce = new Vector2(Random.Range(-50, 50) * 2, 200);
                ball.GetComponent<Rigidbody2D>().velocity = (throwForce.normalized * currentMagnitude);
                ball.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-3f, 3f));

                playTime += Time.deltaTime;
                ballfired = true;
            }
        }
	}

    internal void zeroBlocksLeft()
    {
        endGame = true;
    }

    public void getPoint()
    {
        SoundManager.instance.RandomizeSfx(beep);
        playPoints += 1;
        feedbackPoints.GetComponent<Pisca>().resetBlinking();
    }

    public void ballLost()
    {
        ballsMissed += 1;
        feedbackTime.GetComponent<Pisca>().resetBlinking();
        gameStarted = false;
        ballfired = false;
        innerTimer = 0;
    }

    public float getMagnitude()
    {
        return currentMagnitude;
    }
}
