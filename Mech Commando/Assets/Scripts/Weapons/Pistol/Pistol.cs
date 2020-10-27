using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MainWeapon, IMainWeapon
{
    [SerializeField]
    int HeatCost = 40;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //  if (!canFirePrimary && fireDelayTimer < fireDelay) fireDelayTimer += Time.deltaTime;
        // else { canFirePrimary = true; }

        if (currentHeatLevel > 0)
        {
            currentHeatLevel -= (int)(150 * Time.deltaTime);
        //    Debug.Log(currentHeatLevel);
            // Debug.Log("TEMPO: " + Time.deltaTime);
            if (currentHeatLevel <= 0)
            {
                //Debug.Log(currentHeatLevel);
                //Debug.Log("PODE DISPARAR");
                canFirePrimary = true;
                currentHeatLevel = 0;
            }
        }

        if (currentHeatLevel >= maxHeatLevel)
        {
            //Debug.Log(currentHeatLevel);
            //Debug.Log("SOBREAQUECEU");
            canFirePrimary = false;
        }
      

        
    }

    public override void PrimaryFireStart(WeaponManager weaponManager)
    {
        manager = weaponManager;
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
        if (canFirePrimary)
        {
            currentHeatLevel += HeatCost;
            // Debug.Log($"Primary Weapon Firing: Primary Fire");
            fireDelayTimer = 0;
            //canFirePrimary = false;

            foreach (Transform s in ShootPlaces)
            {
                GameObject a = Instantiate(projectile, s.position, Quaternion.identity);
                Laser l = a.GetComponent<Laser>();
                l.start = s.position;
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
