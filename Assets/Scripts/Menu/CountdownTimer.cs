using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

// Ali Hassan

public class CountdownTimer : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    [SerializeField] private float maxTime = 10f;
    [SerializeField] private SharedHealth health;

    [SerializeField] private TMP_Text UIText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    void Start()
    {
        startTimer = true;
        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        }
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!startTimer) return;

        timerIncrementValue = PhotonNetwork.Time - startTime;
        UIText.text = timerIncrementValue.ToString("0") + " / " + maxTime.ToString("0");

        if (timerIncrementValue >= maxTime)
        {
            // startTimer = false;
            winScreen.SetActive(true);
        }

        if (health.Health.IsDying())
        {
            // startTimer = false;
            loseScreen.SetActive(true);
        }
    }
}