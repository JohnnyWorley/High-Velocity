using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float maxScore = 1000;
    public static TimerScript instance;
    [HideInInspector]
    public bool levelEnded = false;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pauseTimerText;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelEnded)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            float miliseconds = (elapsedTime % 1 * 1000);
            miliseconds = Mathf.Round(miliseconds * 100) / 100f;
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
            pauseTimerText.text = ("Time: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds));
            GameManager.instance.timeBonus = (int)((maxScore - elapsedTime * 5)) * 10;

        }
    }
}
