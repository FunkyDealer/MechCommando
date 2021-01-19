using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Energy : Base_Hud
{
    int Energy;



    protected override void Awake()
    {
        Player.onEnergyUpdate += GetEnergy;
        base.Awake();
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Awake();
        
    }

    void OnDestroy()
    {
        Player.onEnergyUpdate -= GetEnergy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetEnergy(int E, int maxEnergy)
    {
        Energy = E;
        textDisplay.text = $"{text}: {Energy}/{maxEnergy}";
    }

}
