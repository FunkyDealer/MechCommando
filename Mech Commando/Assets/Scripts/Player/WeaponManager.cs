using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    MainWeapon currentPrimary;
    SpecialWeapon currentSpecial;
    public MainWeapon GetCurrentPrimary() => currentPrimary;
    public SpecialWeapon GetCurrentSpecial() => currentSpecial;
    Transform weaponPlace;
    Transform SpecialPlace;

    public int currentPrimaryAmmo;

    public delegate void UpdateAmmoEvent(int a, int maxA, bool isInfinite);
    public static event UpdateAmmoEvent onAmmoUpdate;

    public delegate void UpdateHeatLevel(int l, int maxL);
    public static event UpdateHeatLevel onHeatUpdate;

    Camera cam;
    [SerializeField]
    float maxTargetDistance;
    [SerializeField]
    float minTargetDistance;

    Player player;

    void awake()
    {
        currentPrimaryAmmo = currentPrimary.GetMaxAmmo();
    }

    // Start is called before the first frame update
    void Start()
    {

        SpecialPlace = transform.Find("Main Camera/Special Weapon Place");
        currentSpecial = GetComponentInChildren<SpecialWeapon>();

        weaponPlace = transform.Find("Main Camera/Weapon Place");
        currentPrimary = GetComponentInChildren<MainWeapon>();

        GameObject c = GameObject.Find("Main Camera");
        cam = c.GetComponent<Camera>();

        onAmmoUpdate(currentPrimaryAmmo, currentPrimary.GetMaxAmmo(), currentPrimary.isInfinite);

        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAlive() && player.inControl)
        {

            if (Input.GetButtonDown("PrimaryFire"))
            {
                primaryFireStart();
            }
            if (Input.GetButtonUp("PrimaryFire"))
            {
                primaryFireEnd();
            }

            if (Input.GetButtonDown("SpecialFire"))
            {
                SpecialFireStart();
            }

            if (Input.GetButtonUp("SpecialFire"))
            {
                SpecialFireEnd();
            }


            if (Input.GetButtonDown("SecondaryFire"))
            {
                SecondaryFireStart();
            }
            if (Input.GetButtonUp("SecondaryFire"))
            {
                SecondaryFireEnd();
            }

            if (Input.GetButtonDown("SpecialFire"))
            {
                SpecialFireStart();
            }
            if (Input.GetButtonUp("SpecialFire"))
            {
                SpecialFireEnd();
            }

        }
       
    }

    void LateUpdate()
    {

        weaponPlace.LookAt(calcTarget()); //Corrects the weapon to point at where you are looking

        //Update hud Heat Level
        try
        {
            onHeatUpdate(currentPrimary.GetCurrentHeatLevel(), currentPrimary.GetMaxHeatLevel());
        } catch (NullReferenceException)
        {

        }

    }

    public Vector3 calcTarget() //calculate the target at which you are looking
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        Vector3 target = cam.transform.position + cam.transform.forward * maxTargetDistance;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxTargetDistance, layerMask))
        {
            if (hit.collider)
            {
                float distance2Target = Vector3.Distance(cam.transform.position, hit.point);
                //Debug.Log(distance2Target);

                if (distance2Target > minTargetDistance) target = hit.point;
                else target = cam.transform.position + cam.transform.forward * minTargetDistance;

                // Debug.Log($"looking at {hit.collider.name}");
            }
        }
        
        return target;
    }


    void primaryFireStart() //Pull the trigger
    {
        if (currentPrimary.isInfinite || currentPrimaryAmmo > 0)
        {
            currentPrimary.PrimaryFireStart(this);
            updateAmmo();
        }
    }

    void primaryFireEnd() //Release the trigger
    {
        currentPrimary.PrimaryFireEnd();
    }

    void SecondaryFireStart() //Pull the trigger
    {
            currentPrimary.SecondaryFireStart(this);
            updateAmmo();
    }

    void SecondaryFireEnd() //Release the trigger
    {
        currentPrimary.SecondaryFireEnd();
    }

    void SecondaryFire()
    {

    }



    void SpecialFireStart()
    {
        currentSpecial.Shoot();
    }

    void SpecialFireEnd()
    {

    }


    public void receiveAmmo(int ammount) //Receive ammo from something
    {
        if (currentPrimaryAmmo + ammount < currentPrimary.GetMaxAmmo()) currentPrimaryAmmo += ammount;
        else currentPrimaryAmmo = currentPrimary.GetMaxAmmo();

        updateAmmo();
    }

    public void updateAmmo()
    {
        onAmmoUpdate(currentPrimaryAmmo, currentPrimary.GetMaxAmmo(), currentPrimary.isInfinite);
    }

    public void Switch2NewWeapon(GameObject newWeapon)
    {
        Destroy(currentPrimary.gameObject);
                
        GameObject a = Instantiate(newWeapon, weaponPlace.position, weaponPlace.rotation, weaponPlace);
        //a.transform.parent = weaponPlace;
        currentPrimary = a.GetComponent<MainWeapon>();
        a.transform.localPosition = Vector3.zero;
        a.transform.localRotation = Quaternion.identity;

        currentPrimaryAmmo = currentPrimary.GetMaxAmmo();

    }
}
