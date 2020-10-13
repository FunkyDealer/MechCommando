using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nanoPackPickUp : PickUp
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player p = other.gameObject.GetComponent<Player>();
            p.increaseHpak();
            Destroy(gameObject);
        }
    }

}
