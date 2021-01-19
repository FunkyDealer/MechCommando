using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : PickUp
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
            Player p = other.gameObject.GetComponent<Player>();

            if (p.CurrentShield() < p.MaxShield())
            {
                p.increaseShield(ammount);

                pickUpSound.gameObject.transform.parent = null;
                pickUpSound.PlayOneShot(pickUpClip);
                Destroy(pickUpSound.gameObject, pickUpClip.length);

                Destroy(gameObject);
            }


        }
    }


}
