using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : Weapon
{
    [SerializeField]
    public bool isInfinite; //if the ammo is infinite or not

    protected int currentHeatLevel;
    protected int maxHeatLevel;
    [SerializeField]
    protected string countryOfOrigin;
    protected int accuracy;
    //Secondary Fire

    //Rate of Fire
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
