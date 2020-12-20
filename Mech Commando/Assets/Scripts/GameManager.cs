﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool pause;
    Player p;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p.isAlive() && p.inControl)
        {

            if (Input.GetButtonDown("Pause"))
            {
                if (pause) unPauseGame();
                else pauseGame();
            }

        }



    }

    void pauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pause = true;
    }

    void unPauseGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pause = false;
    }


}
