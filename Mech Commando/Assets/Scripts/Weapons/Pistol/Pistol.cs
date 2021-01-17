using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MainWeapon, IMainWeapon
{
    AudioSource ShotSound;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ShotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        
    }

    public override void PrimaryFireStart(WeaponManager weaponManager)
    {
        base.PrimaryFireStart(weaponManager);
        PrimaryFire();
    }

    public override void PrimaryFireEnd()
    {

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
        if (!overHeated && canFirePrimary)
        {
            ShotSound.Play();
            heatUp();
           // Debug.Log($"Primary Weapon Firing: Primary Fire");
            fireDelayTimer = 0;
            canFirePrimary = false;

            foreach (Transform s in ShootPlaces)
            {
                GameObject a = Instantiate(projectile, s.position, Quaternion.identity);
                Laser l = a.GetComponent<Laser>();
                l.start = s.position;
                l.direction = s.forward;
                l.damage = baseDamage;
                l.shooter = manager.GetPlayer;
            }
        }
    }

    private void SecondaryFire()
    {
        //Debug.Log($"Primary Weapon Firing: Secondary Fire");
    }

    private void ShootPrimary()
    {



    }
}
