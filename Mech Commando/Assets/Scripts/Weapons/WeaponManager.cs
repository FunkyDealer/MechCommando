using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    MainWeapon currentPrimary;
    int currentPrimaryAmmo;



    void awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currentPrimary = GetComponentInChildren<MainWeapon>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PrimaryFire"))
        {
            primaryFireStart();
        }
        if (Input.GetButtonUp("PrimaryFire"))
        {
            primaryFireEnd();
        }


    }


    void primaryFireStart()
    {
        currentPrimary.PrimaryFireStart();



    }

    void primaryFireEnd()
    {
        currentPrimary.PrimaryFireEnd();
    }

    void SecondaryFire()
    {

    }

    void SpecialFire()
    {

    }
}
