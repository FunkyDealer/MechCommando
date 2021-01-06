using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Shield : Base_Hud
{
    int Shield;



    protected override void Awake()
    {
        base.Awake();
        Player.onShieldUpdate += GetShield;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnDestroy()
    {
        Player.onEnergyUpdate -= GetShield;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetShield(int S, int maxShield)
    {
        Shield = S;
        textDisplay.text = $"{text}: {Shield}/{maxShield}";
    }
}
