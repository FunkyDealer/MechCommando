using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Interactible
{
    [SerializeField]
    GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public override void Interact(GameObject actor)
    {
        base.Interact(actor);
        WeaponManager wp = actor.GetComponent<WeaponManager>();

        wp.Switch2NewWeapon(weapon);

        Destroy(transform.parent.gameObject);
    }

}
