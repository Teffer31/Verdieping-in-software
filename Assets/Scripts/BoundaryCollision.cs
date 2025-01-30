using UnityEngine;

public class BoundaryCollision : MonoBehaviour
{
    public Vector3 worldMinBounds;
    public Vector3 worldMaxBounds;
    public CustomRigidbody movement;

    void Update()
    {
        if (movement.isStatic) return;

        Vector3 position = transform.position;
        Vector3 velocity = movement.velocity;

        if (position.x < worldMinBounds.x || position.x > worldMaxBounds.x)
        {
            velocity.x = -velocity.x * movement.bounciness;
            position.x = Mathf.Clamp(position.x, worldMinBounds.x, worldMaxBounds.x);
        }

        if (position.y < worldMinBounds.y || position.y > worldMaxBounds.y)
        {
            velocity.y = -velocity.y * movement.bounciness;
            position.y = Mathf.Clamp(position.y, worldMinBounds.y, worldMaxBounds.y);
        }

        if (position.z < worldMinBounds.z || position.z > worldMaxBounds.z)
        {
            velocity.z = -velocity.z * movement.bounciness;
            position.z = Mathf.Clamp(position.z, worldMinBounds.z, worldMaxBounds.z);
        }

        transform.position = position;
        movement.velocity = velocity;
    }
}