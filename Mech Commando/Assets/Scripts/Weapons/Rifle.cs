﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MainWeapon, IMainWeapon
{
    bool firingPrimary;

    void Awake()
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


        if (firingPrimary) PrimaryFire();

        
    }

    public override void PrimaryFireStart(WeaponManager weaponManager)
    {
        manager = weaponManager;
        PrimaryFire();
        firingPrimary = true;
        


    }

    public override void PrimaryFireEnd()
    {        
        firingPrimary = false;
    }

    public override void SecondaryFireStart(WeaponManager weaponManager)
    {
        SecondaryFire();
    }

    public override void SecondaryFireEnd()
    {

    }

    private void PrimaryFire()
    {
        if (!overHeated && canFirePrimary && manager.currentPrimaryAmmo > 0)
        {
            // Debug.Log($"Primary Weapon Firing: Primary Fire");
            fireDelayTimer = 0;
            canFirePrimary = false;
            manager.currentPrimaryAmmo--;
            manager.updateAmmo();
            heatUp();

            foreach (Transform s in ShootPlaces)
            {
                GameObject a = Instantiate(projectile, s.position, Quaternion.identity);                
                GyroGet g = a.GetComponent<GyroGet>();
                g.direction = transform.forward;
                Debug.Log(g.direction);
                g.damage = baseDamage;
            }
        }
    }

    private void SecondaryFire()
    {
        //Debug.Log($"Primary Weapon Firing: Secondary Fire");
    }

}
