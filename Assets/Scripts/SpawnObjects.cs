using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab to spawn
    public Transform spawnPoint;   // The location where objects will be spawned
    public float spawnInterval = 5f; // Time interval between spawns

    private float timer; // Tracks time between spawns

    void Update()
    {
        // Increment timer by the time elapsed since the last frame
        timer += Time.deltaTime;

        // Check if the timer exceeds the spawn interval
        if (timer >= spawnInterval)
        {
            SpawnObject(); // Spawn the object
            timer = 0f;    // Reset the timer
        }
    }

    void SpawnObject()
    {
        // Instantiate a new object at the spawn point with the same rotation
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);

        // Add a Rigidbody component for physics if the prefab doesn't already have one
        if (spawnedObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
            rb.mass = 1f; // Set default mass
        }

        // Add a BoxCollider for collision if the prefab doesn't already have one
        if (spawnedObject.GetComponent<Collider>() == null)
        {
            spawnedObject.AddComponent<BoxCollider>();
        }
    }
}