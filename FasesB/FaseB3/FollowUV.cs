using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUV : MonoBehaviour {

    MeshRenderer mr;
    Material mat;
    Vector2 offset;

    public GameObject player;

    public float parralax = 2f;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        mr = GetComponent<MeshRenderer>();

         mat = mr.material;

         offset = mat.mainTextureOffset;
	}

	void Update () {
        offset.x = player.transform.position.x / transform.localScale.x * parralax;
        offset.y = player.transform.position.y / transform.localScale.y * parralax;

        mat.mainTextureOffset = offset;
    }
}
