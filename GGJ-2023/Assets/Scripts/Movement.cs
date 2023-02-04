using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 1;
    [SerializeField] private Animator pigAnimatorController; 

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
        if(horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            rigidBody.velocity = new Vector2(horizontal * speed, vertical * speed);

            if(rigidBody.velocity != Vector2.zero)
            {
                pigAnimatorController.SetBool("isWalking", true);
            }
            else
            {
                pigAnimatorController.SetBool("isWalking", false);
            }
        }
    }
}
