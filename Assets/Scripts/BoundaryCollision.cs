using UnityEngine;

public class BoundaryCollision : MonoBehaviour
{
    // Define the minimum and maximum world boundaries (for example, the bottom-left corner and top-right corner of the world)
    public Vector3 worldMinBounds; // Minimum point of the world (bottom-left corner)
    public Vector3 worldMaxBounds; // Maximum point of the world (top-right corner)

    // Reference to the SimpleMovement script that manages the object's velocity
    public SimpleMovement movement; // Reference to the object's SimpleMovement script

    void Update()
    {
        // Get the current position and velocity of the object
        Vector3 position = transform.position;
        Vector3 velocity = movement.velocity;

        // Check the boundaries on the X-axis
        if (position.x < worldMinBounds.x || position.x > worldMaxBounds.x)
        {
            velocity.x = -velocity.x; // Reflect the velocity on the X-axis to bounce the object back
            position.x = Mathf.Clamp(position.x, worldMinBounds.x, worldMaxBounds.x); // Keep the position within the X-axis boundaries
        }

        // Check the boundaries on the Y-axis (if needed for 3D worlds)
        if (position.y < worldMinBounds.y || position.y > worldMaxBounds.y)
        {
            velocity.y = -velocity.y; // Reflect the velocity on the Y-axis to bounce the object back
            position.y = Mathf.Clamp(position.y, worldMinBounds.y, worldMaxBounds.y); // Keep the position within the Y-axis boundaries
        }

        // Check the boundaries on the Z-axis
        if (position.z < worldMinBounds.z || position.z > worldMaxBounds.z)
        {
            velocity.z = -velocity.z; // Reflect the velocity on the Z-axis to bounce the object back
            position.z = Mathf.Clamp(position.z, worldMinBounds.z, worldMaxBounds.z); // Keep the position within the Z-axis boundaries
        }

        transform.position = position; // Update the object's new position
        movement.velocity = velocity; // Update the object's velocity in the movement script
    }
}
