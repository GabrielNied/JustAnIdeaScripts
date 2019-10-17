using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaseA2 : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed = 10f;

    [Range(0f, 1f)]
    public float precisionVariance = 0.5f, lag = 0.5f;

    private bool thinking = true;
    private float currentMagnitude, impactDistance, randomLocationMod, thinkingTimer = 0f, mistake = 0f;
    private Vector2 minScreenBounds, maxScreenBounds, newVelocity, targetLocation, moveVector;
    private GameObject gameManagerA, player, ball, target;

    // Use this for initialization
    void Start () {
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        rb2d = this.transform.GetComponent<Rigidbody2D>();
        gameManagerA = GameObject.Find("GameManagerA");
        player = GameObject.Find("Player");
        randomLocationMod = Random.Range(-precisionVariance, precisionVariance);
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.AIMovement();
    }

    void AIMovement()
    {
        // AI GOES HERE

        if (thinking)
        {
            thinkingTimer += Time.deltaTime;

            if (thinkingTimer > lag * Random.Range(1, mistake))
            {
                thinking = !thinking;
            } else
            {
                return;
            }
        }
        
        ball = GameObject.FindGameObjectWithTag("BallFaseA");
        target = (ball) ? ball : player;
        
        targetLocation = new Vector2(transform.position.x, target.transform.position.y + randomLocationMod * mistake);
        moveVector = Vector2.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);

        transform.position = new Vector2(
            transform.position.x,
            Mathf.Clamp(moveVector.y, minScreenBounds.y + 1.3f, maxScreenBounds.y - 1.3f)
            );
        // AI STOPS HERE
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BallFaseA")
        {
            SoundManager.instance.RandomizeSfx(gameManagerA.GetComponent<GameManagerA2>().beep);

            gameManagerA.GetComponent<GameManagerA2>().increaseMagnitude();
            currentMagnitude = gameManagerA.GetComponent<GameManagerA2>().getMagnitude();

            Rigidbody2D ball = collision.transform.gameObject.GetComponent<Rigidbody2D>();
            impactDistance = Vector2.Distance(this.transform.position, collision.transform.position);

            if (collision.transform.position.y < this.transform.position.y) impactDistance *= -1;
            newVelocity = new Vector2(ball.velocity.x * -1, 2f * impactDistance);

            ball.AddTorque(rb2d.velocity.y * 1 / 3);
            ball.velocity = newVelocity.normalized * currentMagnitude;

            randomLocationMod = Random.Range(-precisionVariance, precisionVariance);

            mistake = 1;
        }
    }

    public void doAThink()
    {
        thinking = true;
        thinkingTimer = 0;

        mistake = (Random.Range(0, 8) >= 7) ? 1.5f : 1;
    }

    public void resetMistake()
    {
        mistake = 1;
    }
}
