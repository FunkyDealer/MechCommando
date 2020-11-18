using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_Prompt : Base_Hud
{



    protected override void Awake()
    {
        base.Awake();
        PlayerMovementManager.onPromptUpdate += GetPrompt;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnDestroy()
    {
        PlayerMovementManager.onPromptUpdate -= GetPrompt;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetPrompt(string prompt)
    {
        if (prompt != null)
        {
            textDisplay.text = $"{text}" + "\n" + $"{prompt}";
        } else
        {
            textDisplay.text = null;
        }
    }



}
