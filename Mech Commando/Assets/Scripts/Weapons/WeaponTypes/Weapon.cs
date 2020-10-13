using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{


    [SerializeField]
    protected int maxAmmo;
    [SerializeField]
    protected int baseDamage;
    protected int range;
    protected int buyingCost;
    protected bool isAvailable;
    protected string description;
    [SerializeField]
    protected string simpleName; //Simple name that can be quickly read, ex: Pistol
    [SerializeField]
    protected string fullName; //Weapon's full name

    public int GetMaxAmmo() => maxAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
