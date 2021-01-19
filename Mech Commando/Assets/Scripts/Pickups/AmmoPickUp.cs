using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField]
    private int ammount;

    AudioSource pickUpSound;    
    AudioClip pickUpClip;

    void Start()
    {
        pickUpSound = GetComponentInChildren<AudioSource>();
        pickUpClip = pickUpSound.clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WeaponManager wp = other.gameObject.GetComponent<WeaponManager>();
            MainWeapon mw = wp.GetCurrentPrimary();
            if (!mw.isInfinite && wp.currentPrimaryAmmo < mw.GetMaxAmmo())
            {
                wp.receiveAmmo(ammount);
                pickUpSound.gameObject.transform.parent = null;
                pickUpSound.PlayOneShot(pickUpClip);
                Destroy(pickUpSound.gameObject, pickUpClip.length);

                Destroy(gameObject);
                
            }      
        }
    }

}
