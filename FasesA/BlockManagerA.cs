using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManagerA : MonoBehaviour {

    public LevelNumbers currentLevel;
    public Material[] blockLevelMaterials;
    public GameObject hitAnimation, destroyAnimation;
    public AudioClip destroyBlock;

    private int totalBlocks = 0;
    private GameObject gameManagerA;

    public void Start()
    {
        gameManagerA = GameObject.Find("GameManagerA");
    }

    public void iExist()
    {
        totalBlocks += 1;
    }

    public void iWasHit(GameObject block, Vector2 point)
    {
        Instantiate(hitAnimation, point, Quaternion.identity, this.transform);

        switch (currentLevel)
        {
            case LevelNumbers.A1:
                gameManagerA.GetComponent<GameManagerA1>().getPoint();
                break;
            case LevelNumbers.A3:
                gameManagerA.GetComponent<GameManagerA3>().getPoint();
                break;
            default:
                break;
        }
    }

    public void iWasHit(GameObject block, Vector3 point)
    {
        Instantiate(hitAnimation, point, Quaternion.identity, this.transform);
        gameManagerA.GetComponent<GameManagerA3>().getPoint();
    }

    public void iWasDestroyed (GameObject block)
    {
        Instantiate(destroyAnimation, block.transform.position, Quaternion.identity, this.transform);
        Object.Destroy(block.gameObject);
        SoundManager.instance.RandomizeSfx(destroyBlock);

        totalBlocks -= 1;

        if (totalBlocks <= 0)
        {
            switch (currentLevel)
            {
                case LevelNumbers.A1:
                    gameManagerA.GetComponent<GameManagerA1>().zeroBlocksLeft();
                    break;
                case LevelNumbers.A3:
                    gameManagerA.GetComponent<GameManagerA3>().zeroBlocksLeft();
                    break;
                default:
                    break;
            }
        }
    }
}
