using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    // Variabele voor snelheid
    public Vector3 velocity;   // De snelheid van het object

    public float gravity = -9.81f;  // Gravitatieversnelling
    //public float moveSpeed = 5f;

    // Update wordt één keer per frame aangeroepen
    void Update()
    {
        // Delta tijd om frame-afhankelijkheid te voorkomen
        float deltaTime = Time.deltaTime;

        // WASD-input ophalen voor beweging in de x- en z-richting
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // De velocity in de horizontale (x) en voorwaartse (z) richting aanpassen
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

        // Normaliseer de bewegingsvector om diagonale bewegingen te beperken
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // Update velocity met de bewegingsrichting vermenigvuldigd met de snelheid
        //velocity.x = moveDirection.x * moveSpeed;
        //velocity.z = moveDirection.z * moveSpeed;

        // Zwaartekracht toepassen op de y-snelheid (neerwaartse kracht)
        velocity.y += gravity * deltaTime;

        // Verminder de snelheid door wrijving of een andere kracht
        velocity *= 0.99f;  // Snelheid neemt met 1% af per frame als voorbeeld.

        // Positie van het object updaten op basis van velocity en deltaTime
        transform.position += velocity * deltaTime;
    }
}
