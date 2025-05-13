using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public InputMap playerControls;
    private InputAction move;
    public LogicScript logic;
    public bool birdIsAlive = true;

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
        }
    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 2.6)
        {
            logic.GameOver();
            birdIsAlive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
        birdIsAlive = false;
    }
}
