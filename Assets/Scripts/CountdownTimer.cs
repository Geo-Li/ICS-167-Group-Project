using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float maxTime = 300f;
    [SerializeField] private Entity player;

    [SerializeField] private TMP_Text UIText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < maxTime)
        {
            currentTime += 1 * Time.deltaTime;
            UIText.text = currentTime.ToString("0") + " / " + maxTime.ToString("0");
        } else
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }

        if (player.IsDead)
        {
            Time.timeScale = 0;
            loseScreen.SetActive(true);
        }
    }
}
