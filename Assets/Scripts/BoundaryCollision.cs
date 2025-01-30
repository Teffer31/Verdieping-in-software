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
        Vector3 position = transform.position;
        Vector3 velocity = movement.velocity;

        if (position.x < worldMinBounds.x || position.x > worldMaxBounds.x)
        {
            velocity.x = -velocity.x * movement.bounciness; // Reflecteer en pas bounciness toe
            position.x = Mathf.Clamp(position.x, worldMinBounds.x, worldMaxBounds.x);
        }

        if (position.y < worldMinBounds.y || position.y > worldMaxBounds.y)
        {
            velocity.y = -velocity.y * movement.bounciness; // Reflecteer en pas bounciness toe
            position.y = Mathf.Clamp(position.y, worldMinBounds.y, worldMaxBounds.y);
        }

        if (position.z < worldMinBounds.z || position.z > worldMaxBounds.z)
        {
            velocity.z = -velocity.z * movement.bounciness; // Reflecteer en pas bounciness toe
            position.z = Mathf.Clamp(position.z, worldMinBounds.z, worldMaxBounds.z);
        }

        transform.position = position;
        movement.velocity = velocity;
    }
}
