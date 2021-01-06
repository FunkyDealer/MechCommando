using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField]
    private int ammount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WeaponManager wp = other.gameObject.GetComponent<WeaponManager>();
            MainWeapon mw = wp.GetCurrentPrimary();
            if (!mw.isInfinite)
            {
                wp.receiveAmmo(ammount);

                Destroy(gameObject);
            }      
        }
    }

}
