using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30;
    public float timer = 30;
    public TMP_Text timerText;

    public TMP_Text pointsText;
    private float points = 0;
    private float threshold;

    public FishSpawner fishSpawner;

    public GameObject NextLevelCanvas;
    public GameObject EndCanvas;
    public TMP_Text levelText;
    private float levelCount = 1;

    public Canvas gameCanvas;
    public TMP_Text pointsPrefab;
    public Gradient colorGradient;

    public SharkSpawner sharkSpawner;

    public void AddPoints(float pointsToAdd, Vector3 position)
    {
        points += pointsToAdd;
        pointsText.text = string.Format("{0:D4} / {1:D4}", (int)points, (int)threshold);

        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameCanvas.transform as RectTransform, Camera.main.WorldToScreenPoint(position), gameCanvas.worldCamera, out canvasPosition);

        TMP_Text floatingPoints = Instantiate(pointsPrefab, Vector3.zero, Quaternion.identity);

        floatingPoints.gameObject.AddComponent<FloatingText>();

        // Set the parent of the instantiated text to the canvas
        floatingPoints.transform.SetParent(gameCanvas.transform, false);
        floatingPoints.rectTransform.anchoredPosition = canvasPosition;

        if (pointsToAdd > 0)
        {
            // Change text color based on points range
            float normalizedPoints = Mathf.Clamp01((pointsToAdd - 80) / (420 - 80)); // Normalize points between 0 and 1
            floatingPoints.color = colorGradient.Evaluate(normalizedPoints);

            floatingPoints.text = "+" + Mathf.RoundToInt(pointsToAdd).ToString();
        } else
        {
            floatingPoints.text = Mathf.RoundToInt(pointsToAdd).ToString();
            floatingPoints.color = Color.white;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        fishSpawner.Start();
        float newThreshold = fishSpawner.CalculateThreshold(timeRemaining);
        sharkSpawner.SetPoints(0.2f * newThreshold);
        threshold += newThreshold;
        pointsText.text = string.Format("{0:D4} / {1:D4}", (int)points, (int)threshold);
        StartCoroutine(ShowNextLevelText());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            AddPoints(threshold - points, new Vector3(0,10,0));
        }
        else if (Input.GetKeyDown("j"))
        {
            AddPoints(threshold - points, new Vector3(0,10,0));
            timeRemaining = 0;
        }
        if (timeRemaining > 0)
        {
            DisplayTime(timeRemaining);
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            // Timer has reached zero
            if (points >= threshold)
            {
                // Player has won
                levelCount++;
                levelText.text = "Level " + levelCount;
                StartCoroutine(ShowNextLevelText());
                fishSpawner.minSpawnInterval *= 0.92f;
                fishSpawner.maxSpawnInterval *= 0.92f;
                timeRemaining = timer;
                float newThreshold = fishSpawner.CalculateThreshold(timeRemaining);
                sharkSpawner.SetPoints(0.2f*newThreshold);
                threshold += newThreshold;
                pointsText.text = string.Format("{0:D4} / {1:D4}", (int)points, (int)threshold);

            }
            else
            {
                // Player has lost
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                gameover();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator ShowNextLevelText()
    {
        NextLevelCanvas.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        NextLevelCanvas.SetActive(false);
        //levelCount += 1;
        // yield return new WaitForSeconds(0.5f);
        // NextLevelCanvas.SetActive(true);
        // yield return new WaitForSeconds(0.5f);
        // NextLevelCanvas.SetActive(false);
        // yield return new WaitForSeconds(0.5f);
        // NextLevelCanvas.SetActive(true);
        // yield return new WaitForSeconds(0.5f);
        // NextLevelCanvas.SetActive(false);
    }

    private void gameover()
    {
        Time.timeScale = 0f;
        EndCanvas.SetActive(true);

    }

    public void AddTimePowerup()
    {
        Debug.Log("timeRemaining og:" + timeRemaining);
        timeRemaining += 5f;
        Debug.Log("new timeRemaining:" + timeRemaining);
    }

}
