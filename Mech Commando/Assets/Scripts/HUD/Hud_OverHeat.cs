using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_OverHeat : Base_Hud
{
    protected override void Awake()
    {
        base.Awake();
       WeaponManager.onHeatUpdate += GetHeat;
    }
    void OnDestroy()
    {
        WeaponManager.onHeatUpdate -= GetHeat;
    }
    void Start()
    {

    }
    void Update()
    {

    }
    void GetHeat(int currentHeat, int maxHeat) 
    {
        textDisplay.text = $"{text}: {currentHeat}/{maxHeat}";
    }

}
