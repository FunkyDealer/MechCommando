using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_OverHeat : Base_Hud
{
    protected override void Awake()
    {
        WeaponManager.onHeatUpdate += GetHeat;
        base.Awake();
      
    }
    void OnDestroy()
    {
        WeaponManager.onHeatUpdate -= GetHeat;
    }
    protected override void Start()
    {
        base.Start();
        
    }
    void Update()
    {

    }
    void GetHeat(int currentHeat, int maxHeat) 
    {
        textDisplay.text = $"{text}: {currentHeat}/{maxHeat}";
    }

}
