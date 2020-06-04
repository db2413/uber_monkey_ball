﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void TimeoutEventHandler();
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public float time;
    private bool timeOut;
    private bool stopTimer;
    public event TimeoutEventHandler TimeoutEvent;
    public int sceneIndex;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.GoalEvent += TimeStop;
        pc.PlayerFalloutEvent += TimeStop;

        // Change the timer length based on the level
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            case 0: // Start
                break;
            case 1: // Overturn
                startTime = 30f;
                break;
            case 2: // Sticks
                startTime = 90f;
                break;
            case 3: // Hairpin
                startTime = 40f;
                break;
            case 4: // Slide
                startTime = 30f;
                break;
            case 5: // Slanted Stairs
                startTime = 40f;
                break;
            case 6: // Ramps
                startTime = 40f;
                break;
            case 7: // Bowl
                startTime = 100f;
                break;
            case 8: // Airplanes
                Debug.Log("Airplane level time set");
                startTime = 45f;
                break;
        }

        time = startTime;
        stopTimer = false;
        timeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopTimer == false)
        {
        time -= Time.deltaTime;
        }

        time = Mathf.Max(time, 0f);

        timerText.text = time.ToString("f1");

        if (time <= 0f & timeOut == false)
        {
            TimeoutEvent?.Invoke();
            timeOut = true;
        }
    }

    public void TimeStop()
    {
        stopTimer = true;
    }
    public void TimeStart()
    {
        stopTimer = false;
    }
}
