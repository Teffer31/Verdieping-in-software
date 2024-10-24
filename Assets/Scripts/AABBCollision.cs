using UnityEngine;

public class AABBCollision : MonoBehaviour
{
    // References to the objects being checked for collisions
    public Transform object1;
    public Transform object2;

    // BoxColliders of the objects to determine their boundaries
    public BoxCollider collider1;
    public BoxCollider collider2;

    // References to the movement scripts for both objects
    public SimpleMovement movement1; // Reference to the movement script for object1
    public SimpleMovement movement2; // Reference to the movement script for object2

    // Half extents of the colliders (width, height, depth divided by 2)
    private Vector3 halfExtents1;
    private Vector3 halfExtents2;

    // Variable to track if there was a collision in the previous frame
    private bool wasColliding = false;

    void Start()
    {
        // Calculate the half extents (half of the size of the collider) for both objects
        halfExtents1 = collider1.bounds.extents;
        halfExtents2 = collider2.bounds.extents;
    }

    void Update()
    {
        float distance = Vector3.Distance(object1.position, object2.position); // Perform a quick distance check between the two objects
        float combinedExtents = (halfExtents1 + halfExtents2).magnitude;  // Calculate the sum of the half extents to compare with the distance

        // If the distance is less than or equal to the combined extents, a collision may occur
        if (distance <= combinedExtents)
        {
            bool isColliding = IsColliding(object1.position, halfExtents1, object2.position, halfExtents2); // Accurately check if there is a collision

            if (isColliding)
            {
                Debug.Log("Collision Detected"); // Log the collision in the console
                
                Vector3 displacement = CalculateDisplacement(object1.position, halfExtents1, object2.position, halfExtents2); // Calculate the minimum displacement to push the objects apart

                // Move both objects in opposite directions based on the displacement vector
                object1.position -= displacement * 0.5f;
                object2.position += displacement * 0.5f;

                // Reflect the velocities of both objects so they move away from each other
                movement1.velocity = Vector3.Reflect(movement1.velocity, displacement.normalized);
                movement2.velocity = Vector3.Reflect(movement2.velocity, displacement.normalized);
            }
            wasColliding = isColliding;  // Store whether there was a collision in this frame
        }
    }

    // Checks if the AABBs (Axis-Aligned Bounding Boxes) of the objects overlap
    bool IsColliding(Vector3 pos1, Vector3 halfExtents1, Vector3 pos2, Vector3 halfExtents2)
    {
        // Check if the objects overlap on the x, y, and z axes
        return (pos1.x - halfExtents1.x < pos2.x + halfExtents2.x && pos1.x + halfExtents1.x > pos2.x - halfExtents2.x) &&
               (pos1.y - halfExtents1.y < pos2.y + halfExtents2.y && pos1.y + halfExtents1.y > pos2.y - halfExtents2.y) &&
               (pos1.z - halfExtents1.z < pos2.z + halfExtents2.z && pos1.z + halfExtents1.z > pos2.z - halfExtents2.z);
    }

    // Calculate how much overlap there is between the two objects and the minimum displacement to correct the collision
    Vector3 CalculateDisplacement(Vector3 pos1, Vector3 halfExtents1, Vector3 pos2, Vector3 halfExtents2)
    {
        // Calculate the overlap on each axis (x, y, z)
        float overlapX = Mathf.Min(pos1.x + halfExtents1.x, pos2.x + halfExtents2.x) - Mathf.Max(pos1.x - halfExtents1.x, pos2.x - halfExtents2.x);
        float overlapY = Mathf.Min(pos1.y + halfExtents1.y, pos2.y + halfExtents2.y) - Mathf.Max(pos1.y - halfExtents1.y, pos2.y - halfExtents2.y);
        float overlapZ = Mathf.Min(pos1.z + halfExtents1.z, pos2.z + halfExtents2.z) - Mathf.Max(pos1.z - halfExtents1.z, pos2.z - halfExtents2.z);

        // Choose the axis with the smallest overlap to minimally move the objects
        if (overlapX < overlapY && overlapX < overlapZ)
        {
            return new Vector3(overlapX, 0, 0); // Displacement along the x-axis
        }
        else if (overlapY < overlapX && overlapY < overlapZ)
        {
            return new Vector3(0, overlapY, 0); // Displacement along the y-axis
        }
        else
        {
            return new Vector3(0, 0, overlapZ); // Displacement along the z-axis
        }
    }
}
