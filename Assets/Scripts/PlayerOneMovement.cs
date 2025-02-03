using UnityEngine;

/// <summary>
/// Controls Player 1 movement using WASD and applies physics interactions.
/// </summary>
public class PlayerOneMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private CustomRigidbody _rigidbody; // Reference to CustomRigidbody

    void Start()
    {
        _rigidbody = GetComponent<CustomRigidbody>();

        if (_rigidbody == null)
        {
            Debug.LogError($"CustomRigidbody is missing on {gameObject.name}");
        }
    }

    void FixedUpdate()
    {
        if (_rigidbody == null) return;

        // Get WASD input
        float moveX = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
        float moveZ = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);

        // Create movement vector
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;

        // Apply movement through velocity
        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
    }
}