using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public InputMap playerControls;
    private InputAction move;

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
        birdRigidBody.linearVelocity = Vector2.up * flapStrength;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
