using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_AreaDisplay : Base_Hud
{
    GameObject warning;

    protected override void Awake()
    {
        Player.onAreaUpdate += GetText;
        base.Awake();
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();        
        warning = transform.Find("Warning").gameObject;
        warning.SetActive(false);
    }

    void OnDestroy()
    {
        Player.onAreaUpdate -= GetText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetText(float timer, bool insideMap, bool alive)
    {
        if (!insideMap)
        {
            if (alive)
            {
                warning.SetActive(true);
                textDisplay.text = $"YOU ARE OUTSIDE \n THE MISSION AREA!\n\n {timer:N2} seconds left\nRETURN NOW!";

            }
            else { textDisplay.text = null; warning.SetActive(false); }


            } else
             {
            
            textDisplay.text = null;
            try
            {
                warning.SetActive(false);
            }
            catch { }
        }

        
    }
}
