using UnityEngine;

public class TimeTextMesh : MonoBehaviour
{
    public TextMesh textMesh; // Assign the TextMesh component in the Inspector
    private float startTime = 90f; // Starting time in seconds
    private float timeRemaining;

    void Start()
    {
        timeRemaining = startTime;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
        }

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        textMesh.text = $"{minutes:00}:{seconds:00}";
    }
}
