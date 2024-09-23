using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    bool AABBOverlap(Vector3 minA, Vector3 maxA, Vector3 minB, Vector3 maxB)
    {
        // Check voor overlap op de x-as
        bool xOverlap = (minA.x <= maxB.x) && (maxA.x >= minB.x);
    
        // Check voor overlap op de y-as
        bool yOverlap = (minA.y <= maxB.y) && (maxA.y >= minB.y);
    
        // Check voor overlap op de z-as
        bool zOverlap = (minA.z <= maxB.z) && (maxA.z >= minB.z);

        // Er is een botsing als er overlap is op alle drie de assen
        return xOverlap && yOverlap && zOverlap;
    }

}
