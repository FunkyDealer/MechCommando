using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : StaticEntity
{




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);

        Debug.Log("Target was hit");
    }

    public override void Die()
    {
        
    }
}
