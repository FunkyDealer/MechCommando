using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirGun : StaticEntity
{
    [SerializeField]
    GameObject destroyedState;

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



       // Debug.Log("");
    }

    public override void Die()
    {
        base.Die();

        Instantiate(destroyedState, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
