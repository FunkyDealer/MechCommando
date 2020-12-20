using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Health : Base_Hud
{
    int Health;


    protected override void Awake()
    {
        base.Awake();
        Player.onHealthUpdate += GetHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDestroy()
    {
        Player.onHealthUpdate -= GetHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetHealth(int H, int maxHealth)
    {
        Health = H;
        textDisplay.text = $"{text}: {Health}/{maxHealth}";
    }
}
