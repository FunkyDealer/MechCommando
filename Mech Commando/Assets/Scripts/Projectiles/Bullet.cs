using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : KineticProjectile
{
    Rigidbody rigidBody;

    protected override void Awake()
    {
        base.Awake();
       
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
       


    }

    void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        Vector3 dir_ = direction.normalized;


        transform.position += direction * velocity * Time.deltaTime;
        transform.forward = direction;
      
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        // Instantiate(Prefab, position, rotation);

        hitEntity = collision.transform.gameObject.GetComponent<Entity>();

        Die();

    }


    protected override void Die()
    {
        if (hitEntity != null) damageEntity();
        // Debug.Log("Destroyed");
        Destroy(gameObject);

    }
}
