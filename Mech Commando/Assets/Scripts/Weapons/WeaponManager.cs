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

    Camera cam;
    [SerializeField]
    float maxTargetDistance;
    [SerializeField]
    float minTargetDistance;


    void awake()
    {
        currentPrimaryAmmo = currentPrimary.GetMaxAmmo();
    }

    // Start is called before the first frame update
    void Start()
    {
     

        weaponPlace = transform.Find("Main Camera/Weapon Place");
        currentPrimary = GetComponentInChildren<MainWeapon>();

        GameObject c = GameObject.Find("Main Camera");
        cam = c.GetComponent<Camera>();

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

    void LateUpdate()
    {

        weaponPlace.LookAt(calcTarget()); //Corrects the weapon to point at where you are looking


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

    void SecondaryFire()
    {

    }

    void SpecialFire()
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
