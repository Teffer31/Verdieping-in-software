using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    // Variabele voor snelheid
    public Vector3 velocity;   // De snelheid van het object

    public float gravity = -9.81f;
    
    // Update wordt één keer per frame aangeroepen
    void Update()
    {
        // Delta tijd om frame-afhankelijkheid te voorkomen
        float deltaTime = Time.deltaTime;

        velocity.y += gravity * deltaTime;
        
        // Verminder de snelheid door wrijving of een andere kracht
        velocity *= 0.99f;  // Snelheid neemt met 1% af per frame als voorbeeld.
        
        // Positie van het object updaten op basis van snelheid
        transform.position += velocity * deltaTime;
    }
}
