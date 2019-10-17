using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBallRollA3 : MonoBehaviour {

    public float rotationSpeed = -10f;

    private Rigidbody2D parentRb;
    private Vector2 moveDirection;

    private void Start()
    {
        parentRb = this.transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        moveDirection = parentRb.velocity.normalized;

        this.transform.Rotate(new Vector3 (0, moveDirection.x * rotationSpeed, moveDirection.y * rotationSpeed), Space.World);
    }
}
