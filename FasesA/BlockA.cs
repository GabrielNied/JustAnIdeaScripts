using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockA : MonoBehaviour {

    public bool indestructible = false;

    [Range (1, 4)]
    public int numberOfHits = 1;
    private GameObject gameManagerA, blockManagerA;

    // Use this for initialization
    void Start()
    {
        blockManagerA = GameObject.Find("BlockManagerA");
        if (!indestructible) blockManagerA.GetComponent<BlockManagerA>().iExist();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "BallFaseA")
        {
            if (indestructible) return;

            if (blockManagerA.GetComponent<BlockManagerA>().currentLevel != LevelNumbers.A3)
            {
                blockManagerA.GetComponent<BlockManagerA>().iWasHit(this.gameObject, collision.contacts[0].point);
            } else
            {
                blockManagerA.GetComponent<BlockManagerA>().iWasHit(
                    this.gameObject, 
                    new Vector3 (collision.contacts[0].point.x, collision.contacts[0].point.y, collision.transform.position.z)
                    );
            }
            
            numberOfHits -= 1;

            if (numberOfHits <= 0)
            {
                blockManagerA.GetComponent<BlockManagerA>().iWasDestroyed(this.gameObject);
            }
            else
            {
                int materialIndex = numberOfHits - 1;
                if (blockManagerA.GetComponent<BlockManagerA>().currentLevel == LevelNumbers.A3)
                {
                    MeshRenderer meshR = this.transform.Find("Model").GetComponent<MeshRenderer>();
                    meshR.material = blockManagerA.GetComponent<BlockManagerA>().blockLevelMaterials[materialIndex];
                } else
                {
                    SpriteRenderer sprite = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
                    sprite.material = blockManagerA.GetComponent<BlockManagerA>().blockLevelMaterials[materialIndex];
                }
            }
                
        }
    }   
}