using UnityEngine;

/**
 * This component moves a player forward constantly and allows turning left or right with input.
 * It also accounts for gravity and supports climbing and grounded behavior.
 */
[RequireComponent(typeof(CharacterController))]
public class CharacterForwardMover : MonoBehaviour
{
    [Tooltip("Speed of forward movement, in meters/second")]
    [SerializeField] private float forwardSpeed = 3.5f;
    [Tooltip("Speed of turning, in degrees/second")]
    [SerializeField] private float turnSpeed = 200.0f;
    [Tooltip("Gravity applied to the player, in meters/second^2")]
    [SerializeField] private float gravity = 9.81f;

    private CharacterController characterController;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Apply gravity if the player is not grounded
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0; // Reset vertical velocity when grounded
        }

        // Move the character forward constantly
        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.deltaTime;
        characterController.Move(forwardMovement + velocity * Time.deltaTime);

        // Allow turning left or right based on player input
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);
    }

    void increaseSpeed()
    {
        forwardSpeed += 1;
    }

    public void stop()
    {
        forwardSpeed = 0;
    }
}
