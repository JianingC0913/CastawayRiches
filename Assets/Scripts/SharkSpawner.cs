using System.Collections;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public GameObject fishPrefab; // The fish prefab to spawn
    public float minSpawnInterval = 7f; // Minimum time interval between spawns
    public float maxSpawnInterval = 13f; // Maximum time interval between spawns

    private float timer; // Timer to track spawning intervals

    private float points = 200f;

    void Start()
    {
        // Initialize the timer with a random value between minSpawnInterval and maxSpawnInterval
        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Decrement the timer
        timer -= Time.deltaTime;

        // Check if it's time to spawn a fish
        if (timer <= 0f)
        {
            // Reset the timer with a new random value between minSpawnInterval and maxSpawnInterval
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Spawn a fish
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        // Generate a random position within the spawn area
        Vector2 spawnPosition = Vector2.zero;
        float randomEdge = Random.Range(0, 2); //0: right, 1: left, 2: bottom

        switch (randomEdge)
        {
            case 0: // Left edge
                spawnPosition = new Vector2(-100, Random.Range(50f, Screen.height * 0.5f));
                break;
            case 1: // Right edge
                spawnPosition = new Vector2(Screen.width + 100, Random.Range(50f, Screen.height * 0.5f));
                break;
                //case 2: // Bottom edge
                //    spawnPosition = new Vector2(Random.Range(0f, Screen.width), 0);
                //    break;
        }

        // Convert screen position to world position
        spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);

        // Instantiate fish at spawn position
        GameObject shark = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);

        SharkMovement sharkMovement = shark.GetComponent<SharkMovement>();
        sharkMovement.isRight = randomEdge == 0 ? true : false;
        sharkMovement.speed = Random.Range(sharkMovement.minSpeed, sharkMovement.maxSpeed);
        sharkMovement.size = Random.Range(sharkMovement.minSize, sharkMovement.maxSize);
        sharkMovement.DeterminePoints(points);
        sharkMovement.fishType = "spawner";
    }

    public void SetPoints(float points)
    {
        this.points = points;
    }
}
