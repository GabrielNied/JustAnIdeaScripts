using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerA2 : MonoBehaviour {

    public float timerWaitUp = 1.5f, timerRelaunch = 2f, playTimeMax = 60, timeLostPerBall = 2f;
    public int startingMagnitude = 6;
    public GameObject ballPrefab;

    public AudioClip trilha, beep;

    private TextMeshProUGUI textTime, textPoints, textLevel;
    private GameObject feedbackPoints, feedbackTime, player, enemy;
    private float playTime = 0f, innerTimer = 0f, playTimeTotal, playTimeLeft, currentMagnitude, baseMagnitude;
    private int playPoints = 0, ballsMissed = 0;
    private bool gameStarted = false, ballfired = false;
    private Vector2 throwForce;

    private LevelFlowManager lfw;

    void Start () {
        textTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        textPoints = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        textLevel = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        feedbackTime = GameObject.Find("TimeLess");
        feedbackPoints = GameObject.Find("PointsPlus");
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        currentMagnitude = startingMagnitude;
        baseMagnitude = startingMagnitude;

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
        textLevel.text = "Speed " + (currentMagnitude).ToString("0.00");

        if (playTimeLeft < 0 || Input.GetKeyDown(KeyCode.Delete))
        {
            SoundManager.instance.musicSource.Stop();
            if (playPoints >= 1) lfw.gameWon(LevelNumbers.A2);
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

            // Game Looparoonie
            if (!ballfired)
            {
                // Define angle e position based on player
                // Instantiate a ball
                GameObject ball = Instantiate(
                    ballPrefab,
                    new Vector3(player.transform.position.x * 0.8f, player.transform.position.y, 0f),
                    Quaternion.identity,
                    this.transform);

                // Fire the ball 
                throwForce = new Vector2(-200, Random.Range(-50, 50) * 2);
                ball.GetComponent<Rigidbody2D>().velocity = (throwForce.normalized * currentMagnitude);
                ball.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-3f, 3f));

                playTime += Time.deltaTime;
                ballfired = true;
            }
        }
	}

    public void getPoint()
    {
        playPoints += 1;
        feedbackPoints.GetComponent<Pisca>().resetBlinking();
        gameStarted = false;
        ballfired = false;
        innerTimer = 0;
        baseMagnitude += 0.5f;
        currentMagnitude = baseMagnitude;
    }

    public void ballLost()
    {
        ballsMissed += 1;
        feedbackTime.GetComponent<Pisca>().resetBlinking();
        gameStarted = false;
        ballfired = false;
        innerTimer = 0;
        currentMagnitude = baseMagnitude;
        enemy.GetComponent<EnemyFaseA2>().resetMistake();
    }

    public void playerHit()
    {
        this.increaseMagnitude();
        enemy.GetComponent<EnemyFaseA2>().doAThink();
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
