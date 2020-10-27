using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_OverHeat : Base_Hud
{
    protected override void Awake()
    {
        base.Awake();
        MainWeapon.onHeatUpdate += GetHeat;
    }
    void OnDestroy()
    {
        MainWeapon.onHeatUpdate -= GetHeat;
    }
    void Start()
    {

    }
    void Update()
    {

    }
    void GetHeat(float currentHeat, float maxHeat) 
    {
        textDisplay.text = $"{text}: {currentHeat}/{maxHeat}";
    }

}
