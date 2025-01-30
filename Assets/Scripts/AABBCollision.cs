using UnityEngine;
using System.Collections.Generic;

public class AABBCollision : MonoBehaviour
{
    [System.Serializable]
    public struct PhysicsObject
    {
        public Transform transform;
        public BoxCollider collider;
        public CustomRigidbody movement;
    }

    public List<PhysicsObject> objects;

    void Update()
    {
        // Loop through all objects to check for collisions
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = i + 1; j < objects.Count; j++)
            {
                CheckCollision(objects[i], objects[j]);
            }
        }
    }

    void CheckCollision(PhysicsObject obj1, PhysicsObject obj2)
    {
        Vector3 halfExtents1 = obj1.collider.bounds.extents;
        Vector3 halfExtents2 = obj2.collider.bounds.extents;

        if (!IsColliding(obj1.transform.position, halfExtents1, obj2.transform.position, halfExtents2))
            return;

        Vector3 displacement = CalculateDisplacement(obj1.transform.position, halfExtents1, obj2.transform.position, halfExtents2);
        Vector3 normal = displacement.normalized;

        // Debugging: Visualize collision normals and displacement
        Debug.DrawLine(obj1.transform.position, obj1.transform.position + normal * 2, Color.red, 1f);
        Debug.Log($"Collision Detected: {obj1.transform.name} -> {obj2.transform.name}, Normal: {normal}");

        // Flatten the normal to avoid upward movement
        if (Mathf.Abs(normal.y) > 0.5f)
        {
            normal.y = 0;
            normal.Normalize();
        }

        if (obj1.movement.isStatic)
        {
            ResolveStaticCollision(obj2, displacement, normal);
        }
        else if (obj2.movement.isStatic)
        {
            ResolveStaticCollision(obj1, -displacement, -normal);
        }
        else
        {
            // Both objects are dynamic
            obj1.transform.position -= displacement * 0.5f;
            obj2.transform.position += displacement * 0.5f;

            ApplyImpulse(obj1, obj2, normal);
        }
    }

    void ResolveStaticCollision(PhysicsObject dynamicObj, Vector3 displacement, Vector3 normal)
    {
        // Adjust position of the dynamic object
        dynamicObj.transform.position += displacement;

        // Reflect velocity, restricting Y-axis momentum
        Vector3 reflectedVelocity = Vector3.Reflect(dynamicObj.movement.velocity, normal);
        reflectedVelocity.y = 0; // Remove Y-axis component
        dynamicObj.movement.velocity = reflectedVelocity * dynamicObj.movement.bounciness;
    }

    bool IsColliding(Vector3 pos1, Vector3 halfExtents1, Vector3 pos2, Vector3 halfExtents2)
    {
        return (pos1.x - halfExtents1.x < pos2.x + halfExtents2.x && pos1.x + halfExtents1.x > pos2.x - halfExtents2.x) &&
               (pos1.y - halfExtents1.y < pos2.y + halfExtents2.y && pos1.y + halfExtents1.y > pos2.y - halfExtents2.y) &&
               (pos1.z - halfExtents1.z < pos2.z + halfExtents2.z && pos1.z + halfExtents1.z > pos2.z - halfExtents2.z);
    }

    Vector3 CalculateDisplacement(Vector3 pos1, Vector3 halfExtents1, Vector3 pos2, Vector3 halfExtents2)
    {
        float overlapX = Mathf.Min(pos1.x + halfExtents1.x, pos2.x + halfExtents2.x) - Mathf.Max(pos1.x - halfExtents1.x, pos2.x - halfExtents2.x);
        float overlapY = Mathf.Min(pos1.y + halfExtents1.y, pos2.y + halfExtents2.y) - Mathf.Max(pos1.y - halfExtents1.y, pos2.y - halfExtents2.y);
        float overlapZ = Mathf.Min(pos1.z + halfExtents1.z, pos2.z + halfExtents2.z) - Mathf.Max(pos1.z - halfExtents1.z, pos2.z - halfExtents2.z);

        if (overlapX < overlapY && overlapX < overlapZ)
            return new Vector3(overlapX, 0, 0); // Resolve along X-axis
        else if (overlapY < overlapX && overlapY < overlapZ)
            return new Vector3(0, overlapY, 0); // Resolve along Y-axis
        else
            return new Vector3(0, 0, overlapZ); // Resolve along Z-axis
    }

    void ApplyImpulse(PhysicsObject obj1, PhysicsObject obj2, Vector3 normal)
    {
        float elasticity = 0.8f;

        float mass1 = obj1.movement.mass;
        float mass2 = obj2.movement.mass;

        Vector3 relativeVelocity = obj2.movement.velocity - obj1.movement.velocity;
        float velocityAlongNormal = Vector3.Dot(relativeVelocity, normal);

        if (velocityAlongNormal > 0) return;

        float impulseMagnitude = -(1 + elasticity) * velocityAlongNormal / (1 / mass1 + 1 / mass2);
        Vector3 impulse = impulseMagnitude * normal;

        obj1.movement.velocity -= impulse / mass1;
        obj2.movement.velocity += impulse / mass2;
    }
}
