using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : Weapon
{
    [SerializeField]
    public bool isInfinite; //if the ammo is infinite or not

    protected int currentHeatLevel;
    protected int maxHeatLevel = 100;
    [SerializeField]
    protected string countryOfOrigin;
    protected int accuracy;
    //Secondary Fire

    //Rate of Fire
    [SerializeField]
    protected float fireDelay;
    protected float fireDelayTimer;
    protected bool canFirePrimary;


    public delegate void UpdateOverHeatEvent(float heat, float maxHeat);
    public static event UpdateOverHeatEvent onHeatUpdate;

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

        onHeatUpdate(currentHeatLevel, maxHeatLevel);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        onHeatUpdate(currentHeatLevel, maxHeatLevel);

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
    public void updateOverHeat()
    {
        onHeatUpdate(currentHeatLevel, maxHeatLevel);
    }
}
