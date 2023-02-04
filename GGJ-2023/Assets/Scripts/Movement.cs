using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;

    [SerializeField] private float speed = 1;

    private float horizontal;
    private float vertical;

    public bool canMove = true;

    public static Movement MovementSingleton;

    void Awake()
    {
        MovementSingleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            rigidBody.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }
}
