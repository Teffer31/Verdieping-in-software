using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    // Variabele voor snelheid
    public Vector3 velocity;   // De snelheid van het object (meter per seconde)
    
    // Update wordt één keer per frame aangeroepen
    void Update()
    {
        // Delta tijd om frame-afhankelijkheid te voorkomen
        float deltaTime = Time.deltaTime;
        
        // Positie van het object updaten op basis van snelheid
        transform.position += velocity * deltaTime;
    }
}
