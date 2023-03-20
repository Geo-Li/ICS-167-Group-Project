using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Ali Hassan

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float maxTime = 300f;
    [SerializeField] private SharedHealth health;
    //[SerializeField] private PlayerHealthS health;

    [SerializeField] private TMP_Text UIText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private bool GameHasEnded;
    
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        Time.timeScale = 1;

        GameHasEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHasEnded)
            return;

        if (!health.Health.IsDying() && currentTime < maxTime)
        {
            currentTime += 1 * Time.deltaTime;
            UIText.text = currentTime.ToString("0") + " / " + maxTime.ToString("0");
        } 
        else
        {
            Time.timeScale = 0f;
            winScreen.SetActive(true);
            GameHasEnded = true;
        }
    }
}
