using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_Ammo : Base_Hud
{
    int currentAmmo;
    int maxAmmo;

    protected override void Awake()
    {
        WeaponManager.onAmmoUpdate += GetAmmo;
        base.Awake();       
    }



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
    }

    void OnDestroy()
    {
        WeaponManager.onAmmoUpdate -= GetAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAmmo(int currentAmmo, int maxAmmo, bool isInfinite)
    {
        if (!isInfinite) textDisplay.text = $"{text}: {currentAmmo}/{maxAmmo}";
        else
        {
            textDisplay.text = $"{text}: {Mathf.Infinity}";
        }

    }
}
