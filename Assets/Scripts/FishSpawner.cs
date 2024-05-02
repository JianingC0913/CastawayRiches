using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab1;
    public GameObject fishPrefab2;
    public GameObject fishPrefab3;
    public GameObject fishPrefab4;
    public GameObject fishPrefab5;
    public GameObject fishPrefab6;
    public GameObject fishPrefab7;
    public float minSpawnInterval = .75f;
    public float maxSpawnInterval = 1f;

    float timer;

    //float[] fish1WeightData = new float[3]; // { startingWeight, weightChange, finalWeight }
    float[] fish1WeightData = new float[3] {20f, -2f, 12f}; 
    float[] fish2WeightData = new float[3] {0f, 2f, 12f};
    float[] fish3WeightData = new float[3] {-1f, 1f, 8f};
    float[] fish4WeightData = new float[3] {-1.6f, 0.8f, 4f};
    float[] fish5WeightData = new float[3] {-1.8f, 0.6f, 3f};
    float[] fish6WeightData = new float[3] {-10f, 2.5f, 5f};
    float[] fish7WeightData = new float[3] {-2.5f, 0.5f, 1f};

    //private float totalWeight;
    private float fish1Probability;
    private float fish2Probability;
    private float fish3Probability;
    private float fish4Probability;
    private float fish5Probability;
    private float fish6Probability;
    private float fish7Probability;
    // Start is called before the first frame update
    public void Start()
    {
        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void CalculateProbabilities()
    {
        float fish3Weight = fish3WeightData[0] < 0 ? 0 : fish3WeightData[0];
        float fish4Weight = fish4WeightData[0] < 0 ? 0 : fish4WeightData[0];
        float fish5Weight = fish5WeightData[0] < 0 ? 0 : fish5WeightData[0];
        float fish6Weight = fish6WeightData[0] < 0 ? 0 : fish6WeightData[0];
        float fish7Weight = fish7WeightData[0] < 0 ? 0 : fish7WeightData[0];
        float totalWeight = fish1WeightData[0] + fish2WeightData[0] + fish3Weight + fish4Weight + fish5Weight + fish6Weight + fish7Weight;

        fish1Probability = fish1WeightData[0] / totalWeight;
        fish2Probability = fish2WeightData[0] / totalWeight;
        fish3Probability = fish3Weight / totalWeight;
        fish4Probability = fish4Weight / totalWeight;
        fish5Probability = fish5Weight / totalWeight;
        fish6Probability = fish6Weight / totalWeight;
        fish7Probability = fish7Weight / totalWeight;

        fish1WeightData[0] += fish1WeightData[1];
        fish2WeightData[0] += fish2WeightData[1];
        fish3WeightData[0] += fish3WeightData[1];
        fish4WeightData[0] += fish4WeightData[1];
        fish5WeightData[0] += fish5WeightData[1];
        fish6WeightData[0] += fish6WeightData[1];
        fish7WeightData[0] += fish7WeightData[1];

        if (fish1WeightData[0] < fish1WeightData[2])
            fish1WeightData[0] = fish1WeightData[2];

        if (fish2WeightData[0] > fish2WeightData[2])
            fish2WeightData[0] = fish2WeightData[2];

        if (fish3WeightData[0] > fish3WeightData[2])
            fish3WeightData[0] = fish3WeightData[2];

        if (fish4WeightData[0] > fish4WeightData[2])
            fish4WeightData[0] = fish4WeightData[2];

        if (fish5WeightData[0] > fish5WeightData[2])
            fish5WeightData[0] = fish5WeightData[2];

        if (fish6WeightData[0] > fish6WeightData[2])
            fish6WeightData[0] = fish6WeightData[2];

        if (fish7WeightData[0] > fish7WeightData[2])
            fish7WeightData[0] = fish7WeightData[2];
    }

    public float CalculateThreshold(float timer)
    {
        CalculateProbabilities();
        float averageSpawnInterval = (minSpawnInterval + maxSpawnInterval) / 2;
        float averageFishPerInterval = (timer / averageSpawnInterval);
        float fish1PointsPerFish = fishPrefab1.GetComponent<FishMovement>().points;
        float fish2PointsPerFish = fishPrefab2.GetComponent<FishMovement>().points;
        float fish3PointsPerFish = fishPrefab3.GetComponent<FishMovement>().points;
        float fish4PointsPerFish = fishPrefab4.GetComponent<FishMovement>().points;
        float fish5PointsPerFish = fishPrefab5.GetComponent<FishMovement>().points;
        float fish6PointsPerFish = fishPrefab6.GetComponent<FishMovement>().points;
        float fish7PointsPerFish = fishPrefab7.GetComponent<FishMovement>().points;

        //Debug.Log("averageSpawnInterva: " + averageSpawnInterval);
        //Debug.Log("averageFishPerInterval: " + averageFishPerInterval);

        //Debug.Log("Fish1PointsPerFish: " + fish1PointsPerFish);
        //Debug.Log("Fish2PointsPerFish: " + fish2PointsPerFish);

        //Debug.Log("Fish1PointsPerFish: " + fish1PointsPerFish);
        //Debug.Log("fish1Probability: " + fish1Probability);

        //Debug.Log("Fish2PointsPerFish: " + fish2PointsPerFish);
        //Debug.Log("fish2Probability: " + fish2Probability);

        Debug.Log("Fish3PointsPerFish: " + fish3PointsPerFish);
        Debug.Log("fish3Probability: " + fish3Probability);

        Debug.Log("Fish4PointsPerFish: " + fish4PointsPerFish);
        Debug.Log("fish4Probability: " + fish4Probability);

        float threshold = averageFishPerInterval *
                          (fish1Probability * fish1PointsPerFish +
                          fish2Probability * fish2PointsPerFish +
                          fish3Probability * fish3PointsPerFish +
                          fish4Probability * fish4PointsPerFish +
                          fish5Probability * fish5PointsPerFish +
                          fish6Probability * fish6PointsPerFish +
                          fish7Probability * fish7PointsPerFish);
        Debug.Log("Threshold: " + threshold);
        return threshold/2;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        // When the timer reaches zero or below, spawn a fish and reset the timer
        if (timer <= 0f)
        {
            SpawnFish();
            // Reset the timer with a new random value between min and max intervals
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnFish()
    {
        //GameObject fishPrefab = Random.value < fish1Probability ? fishPrefab1 : fishPrefab2;
        float randomValue = Random.value;
        //GameObject fishPrefab = randomValue < fish1Probability ? fishPrefab1 : randomValue < fish2Probability ? fishPrefab2 : fishPrefab3;
        GameObject fishPrefab = null;
        List<GameObject> fishList = new List<GameObject> { fishPrefab1, fishPrefab2, fishPrefab3, fishPrefab4, fishPrefab5, fishPrefab6, fishPrefab7 };
        List<float> fishProbability = new List<float> { fish1Probability, fish2Probability, fish3Probability, fish4Probability, fish5Probability, fish6Probability, fish7Probability };
        for (int i = 0; i < fishList.Count; i++)
        {
            if (randomValue < fishProbability[i])
            {
                fishPrefab = fishList[i];
                break;
            }
            else
            {
                randomValue -= fishProbability[i];
            }
        }
        //if (randomValue < fish1Probability)
        //{
        //    fishPrefab = fishPrefab1;
        //}
        //else if (randomValue-fish1Probability < fish2Probability)
        //{
        //    fishPrefab = fishPrefab2;
        //}
        //else
        //{
        //    fishPrefab = fishPrefab3;
        //}

        Vector2 spawnPosition = Vector2.zero;
        float randomEdge = Random.Range(0, 2); //0: right, 1: left, 2: bottom

        switch (randomEdge)
        {
            case 0: // Left edge
                spawnPosition = new Vector2(-100, Random.Range(50f, Screen.height*0.5f));
                break;
            case 1: // Right edge
                spawnPosition = new Vector2(Screen.width+100, Random.Range(50f, Screen.height*0.5f));
                break;
            //case 2: // Bottom edge
            //    spawnPosition = new Vector2(Random.Range(0f, Screen.width), 0);
            //    break;
        }

        // Convert screen position to world position
        spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);

        // Instantiate fish at spawn position
        GameObject fish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        // Set attributes for the fish
        FishMovement fishMovement = fish.GetComponent<FishMovement>();
        fishMovement.isRight = randomEdge == 0 ? true : false;
        fishMovement.speed = Random.Range(fishMovement.minSpeed, fishMovement.maxSpeed);
        fishMovement.size = Random.Range(fishMovement.minSize, fishMovement.maxSize);
        fishMovement.DeterminePoints();
        fishMovement.fishType = "spawner";
        fishMovement.generateOffset(); 
    }
}
