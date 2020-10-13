using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : PickUp
{
    [SerializeField]
    private int ammount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player p = other.gameObject.GetComponent<Player>();

            if (p.CurrentArmor() < p.MaxArmor())
            {
                p.increaseArmor(ammount);
                Destroy(gameObject);
            }


        }
    }


}
