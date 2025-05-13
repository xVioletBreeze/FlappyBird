using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public InputMap playerControls;
    private InputAction move;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioClip hitSound;
    public AudioClip flapSound;
    public AudioClip diveSound;
    public AudioClip dieSound;
    private float yVelocity;
    private bool dive = true;

    private void Awake()
    {
        playerControls = new InputMap();
    }

    private void OnEnable()
    {
        move = playerControls.Bird.Move;
        move.Enable();
        move.performed += Flap;
    }

    private void OnDisable()
    {
        move.performed -= Flap;
        move.Disable();
    }
    private void Flap(InputAction.CallbackContext context)
    {
        Debug.Log("Flap!");
        if (birdIsAlive)
        {
            birdRigidBody.linearVelocity = Vector2.up * flapStrength;
            AudioSource.PlayClipAtPoint(flapSound, Camera.main.transform.position, 0.5f);
            dive = true;
        }
    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (birdIsAlive)
        {
            yVelocity = birdRigidBody.linearVelocity.y;
            RotateBird(yVelocity);
            if (yVelocity < -4.5f)
            {
                BirdDive();
            }
            if (transform.position.y > 2.6)
            {
                BirdCollision();
            }
        }
        
        
        anim.SetBool("isAlive", birdIsAlive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive)
        {
            BirdCollision();
        }
        
    }

    private void BirdDive()
    {
        if (dive)
        {
            AudioSource.PlayClipAtPoint(diveSound, Camera.main.transform.position, 0.5f);
            dive = false;
        }
    }

    private void BirdCollision()
    {
        logic.GameOver();
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 0.5f);
        birdIsAlive = false;
        birdRigidBody.rotation = -90;
        AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position, 0.5f);
    }

    private void RotateBird(float velocity)
    {
        int rotation = (int)velocity;
        birdRigidBody.rotation = rotation * 9;
    }

}
