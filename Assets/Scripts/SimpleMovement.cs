using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity = -9.81f;
    public float drag = 0.99f; // Factor to verminderen snelheid (1 = geen weerstand, 0 = volledige stop)
    public float bounciness = 0.8f; // Hoeveel het object terugkaatst
    public float friction = 0.1f; // Hoeveel wrijving het object heeft

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Voeg zwaartekracht toe aan de y-snelheid
        velocity.y += gravity * deltaTime;

        // Update de positie van het object op basis van de snelheid
        transform.position += velocity * deltaTime;

        // Pas wrijving toe (reduceer snelheid)
        velocity *= Mathf.Max(1 - friction * deltaTime, 0);

        // Elimineer kleine snelheden om oneindige beweging te voorkomen
        if (velocity.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
        }
    }
}