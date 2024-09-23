using UnityEngine;

public class AABBCollision : MonoBehaviour
{
    // Referenties naar de twee objecten waar je de AABB collision voor wilt detecteren
    public Transform object1;
    public Transform object2;

    // Grootte van de colliders (de half-extents van de bounding boxes)
    public Vector3 size1;
    public Vector3 size2;

    void Update()
    {
        if (IsColliding(object1.position, size1, object2.position, size2))
        {
            Debug.Log("Collision Detected");
        }
        else
        {
            Debug.Log("No Collision");
        }
    }

    // Functie om AABB collisions te detecteren
    bool IsColliding(Vector3 pos1, Vector3 size1, Vector3 pos2, Vector3 size2)
    {
        // Controleer of de objecten elkaar overlappen op alle assen
        return (pos1.x - size1.x < pos2.x + size2.x && pos1.x + size1.x > pos2.x - size2.x) &&
               (pos1.y - size1.y < pos2.y + size2.y && pos1.y + size1.y > pos2.y - size2.y) &&
               (pos1.z - size1.z < pos2.z + size2.z && pos1.z + size1.z > pos2.z - size2.z);
    }
}