using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC3 : MonoBehaviour {
    public static PlayerC3 InstancePlayC3;

    private int posicao = 2, opcoes = 3;
    public Transform[] positions;
    public CapsuleCollider capsCol;
    public bool auxBool = true;

    void Start () {
        InstancePlayC3 = this;

        capsCol = GetComponent<CapsuleCollider>();
    }
	
	
	void Update () {
        Move();
        Inv();

    }

    public void Move()
    {
        Teleportar();
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (posicao < opcoes)
            {
                posicao++;
                Teleportar();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (posicao > 1)
            {
                posicao--;
                Teleportar();
            }

        }
    }

    public void Teleportar()
    {
        if (posicao == 1)
        {
            transform.position = positions[0].transform.position;
        }
        else if (posicao == 2)
        {
            transform.position = positions[1].transform.position;
        }
        else if (posicao == 3)
        {
            transform.position = positions[2].transform.position;
        }
    }

    public void Inv()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (auxBool)
            {
                capsCol.enabled = false;
                
                GMC3.InstanceGMC3.invisivel = true;
                auxBool = false;
            }
            else 
            {

                capsCol.enabled = true;
                auxBool = true;
            }
            
        }
    }
}
