using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MainWeapon, IMainWeapon
{
    [SerializeField]
    GameObject laser;
    Transform ShootPlace;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ShootPlace = gameObject.transform.Find("ShootPlace");

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
            GameObject a = Instantiate(laser, ShootPlace.position, Quaternion.identity);
            Laser l = a.GetComponent<Laser>();
            l.start = ShootPlace.position;
            l.direction = transform.forward;
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
