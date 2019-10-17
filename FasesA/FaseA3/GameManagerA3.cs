using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerA3 : MonoBehaviour {

    public float timerWaitUp = 1.5f, timerRelaunch = 2f, playTimeMax = 60, timeLostPerBall = 2f;
    public int startingMagnitude = 6;
    public GameObject ballPrefab;

    public AudioClip trilha, beep;

    private TextMeshProUGUI textTime, textPoints, textLevel;
    private GameObject feedbackPoints, feedbackTime, player, paddle;
    private float playTime = 0f, innerTimer = 0f, playTimeTotal, playTimeLeft, currentMagnitude, launchForce;
    private int playPoints = 0, ballsMissed = 0;
    private bool gameStarted = false, ballfired = false, launchLeft = false, endGame = false;
    private Vector2 throwForce;
    private Vector3 launchPosition;

    private LevelFlowManager lfw;

    void Start () {
        textTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        textPoints = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        textLevel = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        feedbackTime = GameObject.Find("TimeLess");
        feedbackPoints = GameObject.Find("PointsPlus");
        player = GameObject.Find("Player");
        paddle = GameObject.Find("LeftPaddle");
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
        textLevel.text = "Speed " + (currentMagnitude/100).ToString("0.00");

        if (playTimeLeft < 0 || endGame || Input.GetKeyDown(KeyCode.Delete))
        {
            SoundManager.instance.musicSource.Stop();
            if (playPoints > 20) lfw.gameWon(LevelNumbers.A3);
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
                currentMagnitude = startingMagnitude * 1.4f;
            }
            else if (playTimeTotal >= playTimeMax * 2 / 4)
            {
                currentMagnitude = startingMagnitude * 1.2f;
            }
            else if (playTimeTotal >= playTimeMax * 1 / 4)
            {
                currentMagnitude = startingMagnitude * 1.05f;
            }

            // Game Looparoonie
            if (!ballfired)
            {
                // Switch between right and left
                launchPosition = (launchLeft) ? paddle.transform.position : player.transform.position;

                // Define angle e position based on player
                // Instantiate a ball
                GameObject ball = Instantiate(
                    ballPrefab,
                    new Vector3(launchPosition.x * 0.8f, launchPosition.y, launchPosition.z),
                    Quaternion.identity,
                    this.transform);

                // Fire the ball 
                launchForce = (launchLeft) ? 200 : -200;
                throwForce = new Vector2(launchForce, Random.Range(-50, 50) * 2);
                ball.GetComponent<Rigidbody2D>().velocity = (throwForce.normalized * currentMagnitude);
                ball.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-3f, 3f));

                playTime += Time.deltaTime;
                ballfired = true;
                launchLeft = !launchLeft;
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

    public void increaseMagnitude()
    {
        currentMagnitude += 0.25f;
    }

    public float getMagnitude()
    {
        return currentMagnitude;
    }
}
