using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab1; 
    public GameObject powerUpPrefab2;
    public GameObject powerUpPrefab3;

    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 20f;

    public float timer;
    public float lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        Debug.Log("new timer:" + timer);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnPowerups();
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
            Debug.Log("timer reset:" + timer);
        }
    }

    void SpawnPowerups()
    {
        List<GameObject> powerUpList = new List<GameObject> {powerUpPrefab1, powerUpPrefab2, powerUpPrefab3};

        int powerChooser = Random.Range(0, 3);
        Debug.Log("power up spawned, chose:" + powerChooser);
        GameObject newPower = powerUpList[powerChooser];

        Vector2 position = new Vector2(Random.Range(200f, Screen.width - 100), Random.Range(50f, Screen.height * 0.5f));
        position = Camera.main.ScreenToWorldPoint(position);

        GameObject powerUp = Instantiate(newPower, position, Quaternion.identity);
        Debug.Log("power up instantiated at" + position);

        // auto delete powerups that were not picked up
        Destroy(powerUp, lifetime);
        Debug.Log("power up not picked up - auto deleted");
    }
}