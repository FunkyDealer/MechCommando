using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticProjectile : Projectile
{
    protected Vector3 LaunchPoint;
    public float velocity;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (lifeTimer < lifeTime) lifeTimer += Time.deltaTime;
        else
        {
            Die();

        }
    }

    protected virtual void Die()
    {

        Destroy(gameObject);

    }

    protected virtual void Die(Vector3 contactPoint)
    {

        Destroy(gameObject);

    }
}
