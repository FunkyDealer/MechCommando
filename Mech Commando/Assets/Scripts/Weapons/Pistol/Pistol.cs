using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MainWeapon, IMainWeapon
{
    [SerializeField]
    GameObject laser;
    Transform[] ShootPlaces;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ShootPlaces = new Transform[3];
        ShootPlaces[0] = gameObject.transform.Find("ShootPlace1");
        ShootPlaces[1] = gameObject.transform.Find("ShootPlace2");
        ShootPlaces[2] = gameObject.transform.Find("ShootPlace3");

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!canFirePrimary && fireDelayTimer < fireDelay) fireDelayTimer += Time.deltaTime;
        else { canFirePrimary = true; }


    }

    public override void PrimaryFireStart()
    {
        PrimaryFire();

    }

    public override void PrimaryFireEnd()
    {

    }

    public override void SecondaryFireStart()
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
                GameObject a = Instantiate(laser, s.position, Quaternion.identity);
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
