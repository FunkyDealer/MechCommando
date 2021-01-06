using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public int totalSeconds;

    protected float currentTimer;
    protected float timer;

    Text timerDisplay;

    [SerializeField]
    public string timerText;

    [SerializeField]
    protected bool hidden;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        timer = totalSeconds;
        currentTimer = totalSeconds;

      if (!hidden)  timerDisplay = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            Run();
        }
        else
        {
            End();
        }



    }

    protected virtual void End()
    {
        Destroy(gameObject);
    }

    protected virtual void Run()
    {
        if (!hidden) DisplayText();

 
    }

    protected virtual void DisplayText()
    {
        int mins = (int)(currentTimer / 60);
        int seconds = (int)(currentTimer % 60);
        float milliseconds = (float)(currentTimer - Math.Truncate(currentTimer));
        milliseconds *= 1000;
        milliseconds = (float)Math.Truncate(milliseconds);
        string mm = milliseconds.ToString();
        //mm.PadLeft(3, '0');
        if (mm.Length < 3) mm = $"0{mm}";



        timerDisplay.text = $"{timerText}  {mins}:{seconds}:{mm}";
    }

}
