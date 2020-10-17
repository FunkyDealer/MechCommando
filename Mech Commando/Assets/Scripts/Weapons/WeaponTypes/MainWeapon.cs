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

    [SerializeField]
    protected GameObject projectile;

    protected WeaponManager manager;

    protected List<Transform> ShootPlaces;

    void onAwake()
    {

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


}
