using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] float maxTime = 100f;
    float timeLeft;
    Image timerBar;

    void Start()
    {
        gameOverText.SetActive(false);
        gameOverPanel.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            gameOverText.SetActive(true);
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}