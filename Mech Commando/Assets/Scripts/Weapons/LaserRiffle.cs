using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRiffle : MainWeapon, IMainWeapon
{
    bool firingPrimary;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!canFirePrimary && fireDelayTimer < fireDelay) fireDelayTimer += Time.deltaTime;
        else { canFirePrimary = true; }

        if (firingPrimary && canFirePrimary) PrimaryFire();
    }

    public override void PrimaryFireStart(WeaponManager weaponManager)
    {
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
        if (canFirePrimary)
        {
            // Debug.Log($"Primary Weapon Firing: Primary Fire");
            fireDelayTimer = 0;
            canFirePrimary = false;

            foreach (Transform s in ShootPlaces)
            {
                //Debug.Log(s.position);
                GameObject a = Instantiate(projectile, s.TransformPoint(Vector3.zero), Quaternion.identity);
                PhysLaser l = a.GetComponent<PhysLaser>();
                
                // l.transform.position = s.TransformPoint(Vector3.zero);
                // l.transform.position = s.position;
                //l.transform.position = s.localPosition;

                l.direction = transform.forward;
                l.damage = baseDamage;
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
