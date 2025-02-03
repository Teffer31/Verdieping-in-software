using UnityEngine;

/// <summary>
/// Ensures objects remain within the defined world boundaries and bounce upon collision.
/// </summary>
public class BoundaryCollision : MonoBehaviour
{
    public Vector3 worldMinBounds; // Minimum world boundary
    public Vector3 worldMaxBounds; // Maximum world boundary

    public CustomRigidbody movement; // Reference to CustomRigidbody for velocity updates

    void Update()
    {
        Vector3 position = transform.position;
        Vector3 velocity = movement.velocity;

        // Check X-axis boundary
        if (position.x < worldMinBounds.x || position.x > worldMaxBounds.x)
        {
            velocity.x = -velocity.x * movement.bounciness; // Reflect velocity
            position.x = Mathf.Clamp(position.x, worldMinBounds.x, worldMaxBounds.x);
        }

        // Check Y-axis boundary
        if (position.y < worldMinBounds.y || position.y > worldMaxBounds.y)
        {
            velocity.y = -velocity.y * movement.bounciness;
            position.y = Mathf.Clamp(position.y, worldMinBounds.y, worldMaxBounds.y);
        }

        // Check Z-axis boundary
        if (position.z < worldMinBounds.z || position.z > worldMaxBounds.z)
        {
            velocity.z = -velocity.z * movement.bounciness;
            position.z = Mathf.Clamp(position.z, worldMinBounds.z, worldMaxBounds.z);
        }

        transform.position = position;
        movement.velocity = velocity; // Apply velocity update
    }
}