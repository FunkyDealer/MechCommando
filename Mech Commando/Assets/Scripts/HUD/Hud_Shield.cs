using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Shield : Base_Hud
{
    int Shield;

    protected override void Awake()
    {
        Player.onShieldUpdate += GetShield;
        base.Awake();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

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
        if (this != null)
        {
            Shield = S;
            textDisplay.text = $"{text}: {Shield}/{maxShield}";
        }
    }
}
