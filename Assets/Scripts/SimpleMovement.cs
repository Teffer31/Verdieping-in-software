using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity = -9.81f;
    public float drag = 0.99f;  // Factor to reduce velocity (1 = no deceleration, 0 = full stop)

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Add gravity to the y-velocity
        velocity.y += gravity * deltaTime;

        // Update the object's position based on the velocity
        transform.position += velocity * deltaTime;

        // Apply drag/air resistance to reduce the velocity
        velocity *= drag;

        // Optionally: eliminate small velocity to prevent infinite movement
        if (velocity.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
        }
    }
}