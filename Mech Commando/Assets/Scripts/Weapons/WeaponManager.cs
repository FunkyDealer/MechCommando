using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    MainWeapon currentPrimary;
    public MainWeapon GetCurrentPrimary() => currentPrimary;
    Transform weaponPlace;

    public int currentPrimaryAmmo;

    public delegate void UpdateAmmoEvent(int a, int maxA, bool isInfinite);
    public static event UpdateAmmoEvent onAmmoUpdate;

    void awake()
    {
        currentPrimaryAmmo = currentPrimary.GetMaxAmmo();
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponPlace = transform.Find("Main Camera/Weapon Place");
        Debug.Log(weaponPlace);
        currentPrimary = GetComponentInChildren<MainWeapon>();
        onAmmoUpdate(currentPrimaryAmmo, currentPrimary.GetMaxAmmo(), currentPrimary.isInfinite);
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
        if (currentPrimary.isInfinite || currentPrimaryAmmo > 0)
        {
            currentPrimary.PrimaryFireStart(this);

            updateAmmo();
        }
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


    public void receiveAmmo(int ammount)
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
