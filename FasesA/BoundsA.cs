using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsA : MonoBehaviour {

    public bool getPoint = false, losePoint = false, destroy = true, fixSpeed = false;
    public LevelNumbers currentLevel;

    private float speedTreshold;
    private GameObject gameManagerA;

    // Use this for initialization
    void Start () {
        gameManagerA = GameObject.Find("GameManagerA");
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BallFaseA")
        {
            if (destroy) Object.Destroy(collision.gameObject, .5f);
            if (getPoint) this.callGetPointMethod();
            if (losePoint) this.callBallLostMethod();
            if (fixSpeed) this.fixBallSpeed(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BallFaseA")
        {
            if (destroy) Object.Destroy(collision.gameObject, .5f);
            if (getPoint) this.callGetPointMethod();
            if (losePoint) this.callBallLostMethod();
            if (fixSpeed) this.fixBallSpeed(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void callBallLostMethod()
    {
        switch (currentLevel)
        {
            case LevelNumbers.A0:
                if (losePoint) gameManagerA.GetComponent<GameManagerA0>().ballLost();
                break;
            case LevelNumbers.A1:
                if (losePoint) gameManagerA.GetComponent<GameManagerA1>().ballLost();
                break;
            case LevelNumbers.A2:
                if (losePoint) gameManagerA.GetComponent<GameManagerA2>().ballLost();
                break;
            case LevelNumbers.A3:
                if (losePoint) gameManagerA.GetComponent<GameManagerA3>().ballLost();
                break;
            default:
                break;
        }
    }

    private void callGetPointMethod()
    {
        switch (currentLevel)
        {
            case LevelNumbers.A2:
                if (getPoint) gameManagerA.GetComponent<GameManagerA2>().getPoint();
                break;
            default:
                break;
        }
    }

    private void fixBallSpeed(Rigidbody2D ballRB)
    {
        float previousMagnitude = ballRB.velocity.magnitude;

        speedTreshold = 1.0f;
        if (currentLevel == LevelNumbers.A3) speedTreshold = 40f;
        
        if (ballRB.velocity.x <= speedTreshold && ballRB.velocity.x >= -speedTreshold)
        {
            Vector2 newVelocity = new Vector2(speedTreshold * Mathf.Sign(ballRB.velocity.x), ballRB.velocity.y);
            ballRB.velocity = newVelocity.normalized * previousMagnitude;
        }
        else if (ballRB.velocity.y <= speedTreshold && ballRB.velocity.y >= -speedTreshold)
        {
            Vector2 newVelocity = new Vector2(ballRB.velocity.x, speedTreshold * Mathf.Sign(ballRB.velocity.y));
            ballRB.velocity = newVelocity.normalized * previousMagnitude;
        }

    }
}
