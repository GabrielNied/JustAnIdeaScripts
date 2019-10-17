using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed = 10f;
    
    public LevelNumbers currentLevel;
    public bool leftPaddle = false;

    private bool canWalk = true;
    private float currentMagnitude, impactDistance, topBound, botBound;
    private Vector2 minScreenBounds, maxScreenBounds, newVelocity;
    private GameObject gameManagerA;

    // Use this for initialization
    void Start () {
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        rb2d = this.transform.GetComponent<Rigidbody2D>();
        gameManagerA = GameObject.Find("GameManagerA");

        if (currentLevel == LevelNumbers.A3)
        {
            topBound = GameObject.Find("Walls/Top").transform.position.y;
            botBound = GameObject.Find("Walls/Bottom").transform.position.y;
        }

    }

    // Update is called once per frame
    void Update () {
        this.Movement();
    }

    void Movement()
    {
        if (canWalk)
        {
            if (currentLevel == LevelNumbers.A0 || currentLevel == LevelNumbers.A1)
            {
                transform.position = new Vector2(
                   Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.95f, maxScreenBounds.x - 0.95f),
                   transform.position.y
                   );

                float moveHorizontal = Input.GetAxis("Horizontal");
                rb2d.velocity = new Vector2(moveHorizontal * speed, 0);

                if ((transform.position.x <= (minScreenBounds.x + 0.95f) && moveHorizontal < 0) ||
                    (transform.position.x >= (maxScreenBounds.x - 0.95f) && moveHorizontal > 0))
                {
                    rb2d.velocity = Vector2.zero;
                }
            } else if (currentLevel == LevelNumbers.A2)
            {
                transform.position = new Vector2(
                   transform.position.x,
                   Mathf.Clamp(transform.position.y, minScreenBounds.y , maxScreenBounds.y )
                   );

                float moveVertical = Input.GetAxis("Vertical");
                if (leftPaddle) moveVertical *= -1;
                
                rb2d.velocity = new Vector2(0, moveVertical * speed);
                
                if ((transform.position.y <= (minScreenBounds.y + 0.95f) && moveVertical < 0) ||
                    (transform.position.y >= (maxScreenBounds.y - 0.95f) && moveVertical > 0))
                {
                    rb2d.velocity = Vector2.zero;
                }
            } else if (currentLevel == LevelNumbers.A3)
            {
                transform.position = new Vector3(
                   transform.position.x,
                   Mathf.Clamp(transform.position.y, botBound + 25, topBound - 25),
                   transform.position.z
                   );

                float moveVertical = Input.GetAxis("Vertical");
                if (leftPaddle) moveVertical *= -1;

                rb2d.velocity = new Vector2(0, moveVertical * speed);
                
                if ((transform.position.y <= (botBound + 24.5f) && moveVertical < 0) ||
                   (transform.position.y >= (topBound - 24.5f) && moveVertical > 0))
                {
                    rb2d.velocity = Vector2.zero;
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BallFaseA") {

            switch (currentLevel)
            {
                case LevelNumbers.A0:
                    gameManagerA.GetComponent<GameManagerA0>().getPoint();
                    break;
                case LevelNumbers.A1:
                    currentMagnitude = gameManagerA.GetComponent<GameManagerA1>().getMagnitude();
                    SoundManager.instance.RandomizeSfx(gameManagerA.GetComponent<GameManagerA1>().beep);
                    break;
                case LevelNumbers.A2:
                    currentMagnitude = gameManagerA.GetComponent<GameManagerA2>().getMagnitude();
                    SoundManager.instance.RandomizeSfx(gameManagerA.GetComponent<GameManagerA2>().beep);
                    break;
                case LevelNumbers.A3:
                    currentMagnitude = gameManagerA.GetComponent<GameManagerA3>().getMagnitude();
                    SoundManager.instance.RandomizeSfx(gameManagerA.GetComponent<GameManagerA3>().beep);
                    break;
                default:
                    break;
            }
            
            Rigidbody2D ball = collision.transform.gameObject.GetComponent<Rigidbody2D>();
            impactDistance = Vector2.Distance(this.transform.position, collision.transform.position);

            if (currentLevel == LevelNumbers.A0 || currentLevel == LevelNumbers.A1)
            {    
                if (collision.transform.position.x < this.transform.position.x) impactDistance *= -1;
                newVelocity = new Vector2(3.5f * impactDistance, Mathf.Abs(ball.velocity.y));
                ball.AddTorque(rb2d.velocity.x * 1 / 3);
            } else if (currentLevel == LevelNumbers.A2)
            {
                if (collision.transform.position.y < this.transform.position.y) impactDistance *= -1;
                newVelocity = new Vector2(ball.velocity.x * -1, 3f * impactDistance);
                ball.AddTorque(rb2d.velocity.y * 1 / 3);

                gameManagerA.GetComponent<GameManagerA2>().playerHit();
            } else if (currentLevel == LevelNumbers.A3)
            {
                if (collision.transform.position.y < this.transform.position.y) impactDistance *= -1;
                newVelocity = new Vector2(ball.velocity.x * -1, 2 * impactDistance);
                ball.AddTorque(rb2d.velocity.y * 1 / 3);
            }

            if (currentLevel == LevelNumbers.A0)
            {
                float previousMagnitude = ball.velocity.magnitude;
                ball.velocity = newVelocity.normalized * previousMagnitude;
            } else
            {
                ball.velocity = newVelocity.normalized * currentMagnitude;
            }
        }
    }
}
