using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int currentHealth;
    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected string entityName;

    [SerializeField]
    protected MovementInfo info;

    public MovementInfo GetInfo => info;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;

        info.position = transform.position;
        Vector3 forward = transform.forward;
        info.orientation = Mathf.Atan2(forward.x, forward.z);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        info.orientation = Mathf.Atan2(this.transform.forward.x, this.transform.forward.z);
        info.position = this.transform.position;
        info.rotation = this.transform.rotation.z;
    }

    public Vector3 Position() => transform.position;
    public int Health() => currentHealth;


    public virtual void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) Die();
        Debug.Log($"{entityName} received {damage} damage!");
    }

    public virtual void Die()
    {

    }
}
