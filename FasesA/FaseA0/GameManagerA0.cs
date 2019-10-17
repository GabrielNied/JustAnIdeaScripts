using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerA0 : MonoBehaviour {

    public float timerWaitUp = 1.5f, ballInterval = 2.5f, playTimeMax = 60, timeLostPerBall = 2f;
    public GameObject ballPrefab;

    private TextMeshProUGUI textTime, textPoints, textLevel;
    private GameObject feedbackPoints, feedbackTime;
    private float playTime = 0f, innerTimer = 0f, playTimeTotal, currentBallInterval, playTimeLeft;
    private int playPoints = 0, ballsMissed = 0, gameLevel = 0;
    private bool gameStarted = false;
    private Vector2 horizontalPosition, throwForce;

    private LevelFlowManager lfw;

    void Start () {
        textTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        textPoints = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        textLevel = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        feedbackTime = GameObject.Find("TimeLess");
        feedbackPoints = GameObject.Find("PointsPlus");
        currentBallInterval = ballInterval;

        lfw = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();
    }
	
	void Update () {

        if (!lfw.fadeInDone()) return;

        innerTimer += Time.deltaTime;
        
        playTimeLeft = playTimeMax - playTimeTotal - ballsMissed * timeLostPerBall;
        textTime.text = "Time Left: " + playTimeLeft.ToString("0") + "s";
        textPoints.text = "Points: " + playPoints.ToString();
        textLevel.text = "Stage " + gameLevel.ToString();
        
        if (playTimeLeft < 0 || Input.GetKeyDown(KeyCode.Delete))
        {
            if (playPoints > 10) lfw.gameWon(LevelNumbers.A0);
            else lfw.gameLost();
        }

        if (!gameStarted && innerTimer >= timerWaitUp)
        {
            innerTimer = currentBallInterval;
            gameStarted = true;
        }
        else if (gameStarted)
        {
            playTimeTotal += Time.deltaTime;

            if (playTimeTotal >= playTimeMax * 3 / 4)
            {
                gameLevel = 4;
                currentBallInterval = ballInterval * 1 / 4;
            } else if (playTimeTotal >= playTimeMax * 2 / 4)
            {
                gameLevel = 3;
                currentBallInterval = ballInterval * 2 / 4;
            } else if (playTimeTotal >= playTimeMax * 1 / 4)
            {
                gameLevel = 2;
                currentBallInterval = ballInterval * 3 / 4;
            }
            
            // Game Looparoonie
            if (innerTimer >= currentBallInterval)
            {
                // Define angle e position based on difficulty
                horizontalPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.1f, 0.9f), 1.15f));
                switch (gameLevel) {
                    case 0:
                        horizontalPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
                        throwForce = new Vector2(0, -150);
                        gameLevel = 1;
                        break;
                    case 1:
                        throwForce = new Vector2(0, -175);
                        break;
                    case 2:
                        throwForce = new Vector2(0, Random.Range(-225, -175));
                        break;
                    case 3:
                        throwForce = new Vector2(Random.Range(-100, 100), Random.Range(-275, -225));
                        break;
                    case 4:
                        throwForce = new Vector2(Random.Range(-100, 100)*2, Random.Range(-325, -275));
                        break;
                    default:
                        break;
                }

                // Instantiate a ball
                GameObject ball = Instantiate(
                    ballPrefab,
                    new Vector3(horizontalPosition.x, horizontalPosition.y, 0f),
                    Quaternion.identity,
                    this.transform);
                
                // Fire the ball
                ball.GetComponent<Rigidbody2D>().AddForce(throwForce);
                ball.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-3f, 3f));
                playTime += Time.deltaTime;
                innerTimer = 0;
            }
        }
	}

    public void getPoint()
    {
        playPoints += 1;
        feedbackPoints.GetComponent<Pisca>().resetBlinking();
    }

    public void ballLost()
    {
        ballsMissed += 1;
        feedbackTime.GetComponent<Pisca>().resetBlinking();
    }
}
