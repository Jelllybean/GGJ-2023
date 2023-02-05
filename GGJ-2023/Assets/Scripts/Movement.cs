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

    [Header("Audio")]
    [SerializeField] private AudioSource walkingSoundsPlayer;
    [SerializeField] private List<AudioClip> walkingSounds = new List<AudioClip>();
    private int randomIndex = 0;
    private bool isAlreadyPlaying = false;

    void Awake()
    {
        MovementSingleton = this;
    }

    void Update()
    {
        if(canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            rigidBody.simulated = true;
        }
        else
        {
            rigidBody.simulated = false;
            walkingSoundsPlayer.Stop();
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
                if(!isAlreadyPlaying)
                {
                    walkingSoundsPlayer.clip = walkingSounds[randomIndex];
                    walkingSoundsPlayer.Play();
                }
                isAlreadyPlaying = true;
            }
            else
            {
                pigAnimatorController.SetBool("isWalking", false);
                randomIndex = Random.Range(0, walkingSounds.Count);
                isAlreadyPlaying = false;
            }
        }
    }
}
