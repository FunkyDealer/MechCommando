using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : KineticProjectile
{
    public GameObject go;
    void Start()
    {

    }

    protected override void Update()
    {
        base.Update();

        direction = go.transform.position - this.transform.position;
        this.transform.position += direction * Time.deltaTime * velocity;
    }

    protected override void Die()
    {
        base.Die();
    }
}
