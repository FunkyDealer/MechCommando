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

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Position() => transform.position;
    public int Health() => currentHealth;


    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) Die();
        Debug.Log($"{entityName} received {damage} damage!");
    }

    public virtual void Die()
    {

    }
}
