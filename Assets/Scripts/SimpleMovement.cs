using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity = -9.81f;
    public float drag = 0.99f;  // Factor om de snelheid te verminderen (1 = geen vertraging, 0 = volledige stop)

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Voeg zwaartekracht toe aan de y-snelheid
        velocity.y += gravity * deltaTime;

        // Update de positie van het object op basis van de snelheid
        transform.position += velocity * deltaTime;

        // Pas wrijving/luchtweerstand toe om de snelheid te verminderen
        velocity *= drag;

        // Optioneel: kleine snelheid elimineren om oneindige beweging te voorkomen
        if (velocity.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
        }
    }
}