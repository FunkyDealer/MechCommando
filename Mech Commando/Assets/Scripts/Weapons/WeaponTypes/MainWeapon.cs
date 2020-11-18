using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : Weapon
{
    [SerializeField]
    public bool isInfinite; //if the ammo is infinite or not

    protected int currentHeatLevel;
    public int GetCurrentHeatLevel() => currentHeatLevel;
    protected int maxHeatLevel = 100;
    protected enum HeatState
    {
        NONE,
        HEATED,
        COOLING
    }
    protected bool overHeated;
    [SerializeField]
    protected HeatState heatState;
    protected float coolingDownInterval = 0.5f;
    protected float coolingDownIntervalTimer;
    protected float gunCoolingTimer;
    protected float gunCoolingTime = 0.1f;
    [SerializeField]
    protected int heatCost;
    public int GetMaxHeatLevel() => maxHeatLevel;
    [SerializeField]
    protected string countryOfOrigin;
    protected int accuracy;
    //Secondary Fire

    //Rate of Fire
    [SerializeField]
    protected float fireDelay;
    protected float fireDelayTimer;
    protected bool canFirePrimary;

    [SerializeField]
    protected GameObject projectile;

    protected WeaponManager manager;

    protected List<Transform> ShootPlaces;

    void onAwake()
    {
        overHeated = false;
        heatState = HeatState.NONE;
        coolingDownIntervalTimer = 0;
        gunCoolingTime = 0;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ShootPlaces = new List<Transform>();
        FindShootPlaces();
        canFirePrimary = true;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!overHeated && !canFirePrimary && fireDelayTimer < fireDelay) fireDelayTimer += Time.deltaTime;
        else { canFirePrimary = true; }

        updateHeat();

       
    }

    protected void FindShootPlaces()
    {
        int nrOfChildren = transform.childCount;
        for (int i = 0; i < nrOfChildren; i++)
        {
            Transform c = transform.GetChild(i);
            if (c.gameObject.name == "ShootPlace") ShootPlaces.Add(c);
        }
    }

    public virtual void PrimaryFireStart(WeaponManager weaponManager)
    {
        this.manager = weaponManager;

    }

    public virtual void PrimaryFireEnd()
    {

    }

    public virtual void SecondaryFireStart(WeaponManager weaponManager)
    {
        
    }

    public virtual void SecondaryFireEnd()
    {

    }




    protected void updateHeat()
    {
        if (currentHeatLevel > 0)
        {
            switch (heatState)
            {
                case HeatState.HEATED:

                    if (coolingDownIntervalTimer < coolingDownInterval) coolingDownIntervalTimer += Time.deltaTime;
                    else
                    {
                        coolingDownIntervalTimer = 0;
                        heatState = HeatState.COOLING;
                    }

                    break;
                case HeatState.COOLING:

                    if (gunCoolingTimer < gunCoolingTime) gunCoolingTimer += Time.deltaTime;
                    else
                    {
                        currentHeatLevel--;
                        gunCoolingTimer = 0;
                    }

                    break;         
            }     
        }
        else
        {
            heatState = HeatState.NONE;
            coolingDownIntervalTimer = 0;
            currentHeatLevel = 0;
            overHeated = false;
            canFirePrimary = true;
        }


        if (overHeated)
        {
            PrimaryFireEnd();

        }
    }

    protected void heatUp()
    {
        currentHeatLevel += heatCost;
        coolingDownIntervalTimer = 0;
        heatState = HeatState.HEATED;
        if (currentHeatLevel >= maxHeatLevel)
        {
            canFirePrimary = false;
            overHeated = true;
            currentHeatLevel = maxHeatLevel;
        }
        // Debug.Log($"Primary Weapon Firing: Primary Fire");
    }


}
