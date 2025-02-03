using UnityEngine;
using System.Collections.Generic;

public class AABBCollisionManager : MonoBehaviour
{
    [System.Serializable]
    public struct PhysicsObject
    {
        public Transform transform;
        public BoxCollider collider;
        public CustomRigidbody movement;
    }

    public List<PhysicsObject> objects;

    void FixedUpdate()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = i + 1; j < objects.Count; j++)
            {
                CheckAndResolveCollision(objects[i], objects[j]);
            }
        }
    }

    void CheckAndResolveCollision(PhysicsObject obj1, PhysicsObject obj2)
    {
        Vector3 halfExtents1 = obj1.collider.bounds.extents;
        Vector3 halfExtents2 = obj2.collider.bounds.extents;

        if (!IsColliding(obj1.transform.position, halfExtents1, obj2.transform.position, halfExtents2))
            return;

        // Calculate displacement to separate the objects
        Vector3 displacement = CalculateDisplacement(obj1.transform.position, halfExtents1, obj2.transform.position, halfExtents2);
        Vector3 normal = displacement.normalized;

        // Ensure complete separation
        SeparateObjects(obj1, obj2, displacement);

        // Apply physics response after separation
        if (obj1.movement.isStatic)
        {
            ReflectVelocity(obj2, normal);
        }
        else if (obj2.movement.isStatic)
        {
            ReflectVelocity(obj1, -normal);
        }
        else
        {
            ApplyImpulse(obj1, obj2, normal);
        }
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
            return new Vector3(pos1.x > pos2.x ? overlapX : -overlapX, 0, 0);
        else if (overlapY < overlapX && overlapY < overlapZ)
            return new Vector3(0, pos1.y > pos2.y ? overlapY : -overlapY, 0);
        else
            return new Vector3(0, 0, pos1.z > pos2.z ? overlapZ : -overlapZ);
    }

    void SeparateObjects(PhysicsObject obj1, PhysicsObject obj2, Vector3 displacement)
    {
        // Ensure complete separation until no overlap exists
        while (IsColliding(obj1.transform.position, obj1.collider.bounds.extents, obj2.transform.position, obj2.collider.bounds.extents))
        {
            if (obj1.movement.isStatic)
            {
                obj2.transform.position += displacement.normalized * 0.01f;
            }
            else if (obj2.movement.isStatic)
            {
                obj1.transform.position -= displacement.normalized * 0.01f;
            }
            else
            {
                obj1.transform.position -= displacement.normalized * 0.005f;
                obj2.transform.position += displacement.normalized * 0.005f;
            }
        }
    }

    void ReflectVelocity(PhysicsObject dynamicObj, Vector3 normal)
    {
        dynamicObj.movement.velocity = Vector3.Reflect(dynamicObj.movement.velocity, normal) * dynamicObj.movement.bounciness;
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
