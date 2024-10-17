using UnityEngine;

public class AABBCollision : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    public BoxCollider collider1;
    public BoxCollider collider2;

    public SimpleMovement movement1; // Verwijzing naar het movement script van object1
    public SimpleMovement movement2; // Verwijzing naar het movement script van object2

    private Vector3 halfExtents1;
    private Vector3 halfExtents2;

    private bool wasColliding = false;

    void Start()
    {
        halfExtents1 = collider1.bounds.extents;
        halfExtents2 = collider2.bounds.extents;
    }

    void Update()
    {
        // Voer eerst een snelle afstandscheck uit
        float distance = Vector3.Distance(object1.position, object2.position);
        float combinedExtents = (halfExtents1 + halfExtents2).magnitude;

        if (distance <= combinedExtents)
        {
            bool isColliding = IsColliding(object1.position, halfExtents1, object2.position, halfExtents2);

            if (isColliding)
            {
                Debug.Log("Collision Detected");

                // Bereken de minimale verplaatsing om de objecten uit elkaar te duwen
                Vector3 displacement = CalculateDisplacement(object1.position, halfExtents1, object2.position, halfExtents2);

                // Verplaats beide objecten in tegengestelde richting
                object1.position -= displacement * 0.5f;
                object2.position += displacement * 0.5f;

                // Reflecteer de snelheid zodat objecten van elkaar af bewegen
                movement1.velocity = Vector3.Reflect(movement1.velocity, displacement.normalized);
                movement2.velocity = Vector3.Reflect(movement2.velocity, displacement.normalized);
            }

            wasColliding = isColliding;
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
        // Bereken hoeveel overlap er is tussen de objecten
        float overlapX = Mathf.Min(pos1.x + halfExtents1.x, pos2.x + halfExtents2.x) - Mathf.Max(pos1.x - halfExtents1.x, pos2.x - halfExtents2.x);
        float overlapY = Mathf.Min(pos1.y + halfExtents1.y, pos2.y + halfExtents2.y) - Mathf.Max(pos1.y - halfExtents1.y, pos2.y - halfExtents2.y);
        float overlapZ = Mathf.Min(pos1.z + halfExtents1.z, pos2.z + halfExtents2.z) - Mathf.Max(pos1.z - halfExtents1.z, pos2.z - halfExtents2.z);

        // Kies de as met de kleinste overlap om te verplaatsen
        if (overlapX < overlapY && overlapX < overlapZ)
        {
            return new Vector3(overlapX, 0, 0);
        }
        else if (overlapY < overlapX && overlapY < overlapZ)
        {
            return new Vector3(0, overlapY, 0);
        }
        else
        {
            return new Vector3(0, 0, overlapZ);
        }
    }
}
