using UnityEngine;

public class BoundaryCollision : MonoBehaviour
{
    public Vector3 worldMinBounds; // Minimaal punt van de wereld (linkeronderhoek)
    public Vector3 worldMaxBounds; // Maximaal punt van de wereld (rechterbovenhoek)

    public SimpleMovement movement; // Verwijzing naar het SimpleMovement script van het object

    void Update()
    {
        Vector3 position = transform.position;
        Vector3 velocity = movement.velocity;

        // Controleer de randen op de X-as
        if (position.x < worldMinBounds.x || position.x > worldMaxBounds.x)
        {
            // Reflecteer de snelheid op de X-as (terugkaatsing)
            velocity.x = -velocity.x;

            // Pas de positie aan zodat het object niet buiten de grenzen blijft
            position.x = Mathf.Clamp(position.x, worldMinBounds.x, worldMaxBounds.x);
        }

        // Controleer de randen op de Y-as (indien nodig voor 3D-wereld)
        if (position.y < worldMinBounds.y || position.y > worldMaxBounds.y)
        {
            velocity.y = -velocity.y;
            position.y = Mathf.Clamp(position.y, worldMinBounds.y, worldMaxBounds.y);
        }

        // Controleer de randen op de Z-as
        if (position.z < worldMinBounds.z || position.z > worldMaxBounds.z)
        {
            velocity.z = -velocity.z;
            position.z = Mathf.Clamp(position.z, worldMinBounds.z, worldMaxBounds.z);
        }

        // Werk de nieuwe positie en snelheid bij
        transform.position = position;
        movement.velocity = velocity;
    }
}