using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity = -9.81f; // Standard gravity
    public float drag = 0.99f; // Reduces velocity over time
    public float bounciness = 0.8f; // Controls bounce intensity
    public float friction = 0.1f; // Simulates surface resistance
    public float mass = 1f; // Mass for physics calculations
    public bool isStatic = false; // Determines if the object moves

    void FixedUpdate()
    {
        if (isStatic) return; // Static objects don't move

        float deltaTime = Time.deltaTime;

        // Apply gravity to the Y-axis
        velocity.y += gravity * deltaTime;

        // Calculate the next position
        Vector3 nextPosition = transform.position + velocity * deltaTime;

        // Raycast to detect potential collisions along the path
        RaycastHit hit;
        if (Physics.Linecast(transform.position, nextPosition, out hit))
        {
            // Handle collision
            Vector3 normal = hit.normal;
            velocity = Vector3.Reflect(velocity, normal) * bounciness;

            // Adjust position to avoid clipping
            transform.position = hit.point + normal * 0.01f; // Small offset to prevent overlap
        }
        else
        {
            // No collision, proceed with movement
            transform.position = nextPosition;
        }

        // Apply drag
        velocity *= Mathf.Max(1 - friction * deltaTime, 0);

        // Stop tiny movements
        if (velocity.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
        }
    }
}