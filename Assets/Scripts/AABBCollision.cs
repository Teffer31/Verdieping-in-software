using UnityEngine;
using System.Collections.Generic;

public class AABBCollision : MonoBehaviour
{
    [System.Serializable]
    public struct PhysicsObject
    {
        public Transform transform;     // Referentie naar de transform
        public BoxCollider collider;    // Referentie naar de collider
        public SimpleMovement movement; // Referentie naar het SimpleMovement-script
    }

    public List<PhysicsObject> objects; // Lijst van physics-objecten

    void Update()
    {
        // Controleer botsingen tussen elk paar objecten
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = i + 1; j < objects.Count; j++)
            {
                // Bereken de actuele half extents van de objecten
                Vector3 halfExtents1 = objects[i].collider.bounds.extents;
                Vector3 halfExtents2 = objects[j].collider.bounds.extents;

                CheckCollision(objects[i], objects[j], halfExtents1, halfExtents2);
            }
        }
    }

    void CheckCollision(PhysicsObject obj1, PhysicsObject obj2, Vector3 extents1, Vector3 extents2)
    {
        float distance = Vector3.Distance(obj1.transform.position, obj2.transform.position);
        float combinedExtents = (extents1 + extents2).magnitude;

        if (distance <= combinedExtents)
        {
            bool isColliding = IsColliding(obj1.transform.position, extents1, obj2.transform.position, extents2);

            if (isColliding)
            {
                Debug.Log($"Collision Detected between {obj1.transform.name} and {obj2.transform.name}");

                Vector3 displacement = CalculateDisplacement(obj1.transform.position, extents1, obj2.transform.position, extents2);

                // Verplaats beide objecten
                obj1.transform.position -= displacement * 0.5f;
                obj2.transform.position += displacement * 0.5f;

                // Reflecteer de snelheid
                obj1.movement.velocity = Vector3.Reflect(obj1.movement.velocity, displacement.normalized);
                obj2.movement.velocity = Vector3.Reflect(obj2.movement.velocity, displacement.normalized);
            }
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
            return new Vector3(overlapX, 0, 0);
        else if (overlapY < overlapX && overlapY < overlapZ)
            return new Vector3(0, overlapY, 0);
        else
            return new Vector3(0, 0, overlapZ);
    }
}
