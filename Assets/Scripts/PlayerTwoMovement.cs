using UnityEngine;

/// <summary>
/// Controls Player 2 movement using Arrow Keys and applies physics interactions.
/// </summary>
public class PlayerTwoMovement : MonoBehaviour
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

        // Get Arrow key input
        float moveX = (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
        float moveZ = (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) + (Input.GetKey(KeyCode.UpArrow) ? -1 : 0);

        // Create movement vector
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;

        // Apply movement through velocity
        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
    }
}