using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : Weapon
{
    public int heatLevel;
    public string countryOfOrigin;
    public int accuracy;
    public int rateOfFire;
    public int recoil;
    //Secondary Fire

    [SerializeField]
    protected float fireDelay;
    protected float fireDelayTimer;
    protected bool canFirePrimary;


    void onAwake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        canFirePrimary = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       



    }

    public virtual void PrimaryFireStart()
    {
        

    }

    public virtual void PrimaryFireEnd()
    {

    }

    public virtual void SecondaryFireStart()
    {
        
    }

    public virtual void SecondaryFireEnd()
    {

    }


}
