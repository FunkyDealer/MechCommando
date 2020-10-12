using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected bool isInfinite; //if the ammo is infinite or not
    protected int currentAmmo;
    protected int maxAmmo;
    protected int baseDamage;
    protected int range;
    protected int buyingCost;
    protected bool isAvailable;
    protected string description;
    protected string simpleName; //Simple name that can be quickly read, ex: Pistol
    protected string fullName; //Weapon's full name



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
