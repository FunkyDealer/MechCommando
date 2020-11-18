using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : Entity
{
    [SerializeField]
    protected float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
