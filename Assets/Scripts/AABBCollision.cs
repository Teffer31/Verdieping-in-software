using UnityEngine;

public class AABBCollision : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    public BoxCollider collider1;
    public BoxCollider collider2;

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

            if (isColliding && !wasColliding)
            {
                Debug.Log("Collision Detected");
            }
            else if (!isColliding && wasColliding)
            {
                Debug.Log("No Collision");
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
}