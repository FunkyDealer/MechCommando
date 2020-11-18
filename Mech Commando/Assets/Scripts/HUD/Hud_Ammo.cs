using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_Ammo : Base_Hud
{
    int currentAmmo;
    int maxAmmo;

    protected override void Awake()
    {
        base.Awake();
        WeaponManager.onAmmoUpdate += GetAmmo;
    }

    void OnDestroy()
    {
        WeaponManager.onAmmoUpdate -= GetAmmo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
