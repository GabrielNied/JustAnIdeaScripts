using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Caixa : MonoBehaviour {

    private GameObject gameManager;

    private bool colidiuPlayer = false;

    void Start () {
        gameManager = GameObject.Find("GameManager");
    }
	
	void Update () {
        if(gameManager.GetComponent<GameManagerFaseB0>().vida <= 0 || gameManager.GetComponent<GameManagerFaseB0>().score <= 0)
        {
            colidiuPlayer = true;
            Destroy(this.gameObject);
        }


        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -50, Camera.main.farClipPlane / 2));
        if (this.transform.position.y <= screenPosition.y)
        {
            if (colidiuPlayer)
                return;
            gameManager.GetComponent<GameManagerFaseB0>().lossPoint();
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colidiuPlayer = true;
        }
    }
}
