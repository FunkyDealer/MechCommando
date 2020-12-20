using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : Entity
{
    [SerializeField, Tooltip("max Velocity")]
    public float speed;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public virtual void getDamage(int damage)
    {
        currentHealth -= damage;
        checkHealth();

    }

    protected virtual void checkHealth()
    {
        if (currentHealth <= 0) Die();
    }

    public override void Die()
    {
        base.Die();
        /*
        if (manager == null)
        {
            Debug.Log("this Enemy wasn't associated to any manager");
        }
        else
        {
            manager.RemoveEnemy(this);
        }
        */

        Destroy(gameObject);
    }

    public float Speed() => speed;
}
