using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerC1 : MonoBehaviour {

    public static GameManagerC1 gmC1Ref;

    public float speed1, speed2, speed3, timeSpawn1, timeSpawn2, timeSpawn3, qntInvi = 1;
    public bool invisivel = false, podeInvBar = true, auxBol = true;
    public Text cogumeloText;
    public int cogumelosParaVencer = 0;
    public LevelFlowManager lfw;
    public AudioClip trilha1;


    public GameObject[] plataformasC1;

    

	void Start () {
        gmC1Ref = this;
        //StartCoroutine(SpawnPlataforma1());
        //StartCoroutine(SpawnPlataforma2());
        //StartCoroutine(SpawnPlataforma3());
        SoundManager.instance.musicSource.clip = trilha1;
        SoundManager.instance.musicSource.Play();

        lfw = GameObject.FindGameObjectWithTag("LevelFlowManager").GetComponent<LevelFlowManager>();



    }
	
	
	void Update () {

        if (invisivel)
        {
            qntInvi -= 0.001f;
        }

        if (qntInvi <=0 && auxBol)
        {
            podeInvBar = false;
            
            foreach (GameObject sapinhos in PlayerC1.InstancePlayerC1.sapos)
            {
                Physics2D.IgnoreCollision(sapinhos.GetComponent<Collider2D>(), GameObject.FindWithTag("PlayerC1").GetComponent<Collider2D>(), false);
            }
            PlayerC1.InstancePlayerC1.playerRenderer.color = Color.white;
            PlayerC1.InstancePlayerC1.imagemPreta.SetActive(false);
            invisivel = false;
            PlayerC1.InstancePlayerC1.trailEscuro.SetActive(false);
            PlayerC1.InstancePlayerC1.trailClaro.SetActive(true);
            PlayerC1.InstancePlayerC1.particulaAviso.SetActive(false);

            GameObject particleAux2 = Instantiate(PlayerC1.InstancePlayerC1.particulaFumaca2, PlayerC1.InstancePlayerC1.transform.position, Quaternion.identity);
            particleAux2.transform.position = PlayerC1.InstancePlayerC1.transform.position;
            Destroy(particleAux2, 2f);
            auxBol = false;
        }

        cogumeloText.text = "Mushroom: " + cogumelosParaVencer + "/5";
        if (cogumelosParaVencer == 5 || Input.GetKeyDown(KeyCode.Delete))
        {
            SoundManager.instance.musicSource.Stop();
            lfw.gameWon(LevelNumbers.C1);
        }
    }

    IEnumerator SpawnPlataforma1()
    {
        while (true)
        {
            //Plataforma1 
            Transform transformAux1 = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject platRefInstance1 = Instantiate(plataformasC1[0], transformAux1);
            platRefInstance1.transform.position = new Vector2(9, Random.Range(-4, 4));

            yield return new WaitForSeconds(timeSpawn1);
            
        }
    }
    IEnumerator SpawnPlataforma2()
    {
        while (true)
        {
            //Plataforma2
            Transform transformAux2 = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject platRefInstance2 = Instantiate(plataformasC1[1], transformAux2);
            platRefInstance2.transform.position = new Vector2(9, Random.Range(-5, 5));

            yield return new WaitForSeconds(timeSpawn2);

        }
    }

    IEnumerator SpawnPlataforma3()
    {
        while (true)
        {
            //Plataforma3
            Transform transformAux3 = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject platRefInstance3 = Instantiate(plataformasC1[2], transformAux3);
            platRefInstance3.transform.position = new Vector2(9, Random.Range(-5, 5));

            yield return new WaitForSeconds(timeSpawn3);

        }
    }

}
