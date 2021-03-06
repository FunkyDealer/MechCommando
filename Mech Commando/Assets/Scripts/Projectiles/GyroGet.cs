﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroGet : KineticProjectile
{
    [SerializeField]
    int fuel;

    Rigidbody rigidBody;
    float fuelSpendingTimer;
    [SerializeField]
    float fuelSpendingTime;

    float velocitySpendingTimer;
    [SerializeField]
    float velocitySpendingTime;

    [SerializeField]
    GameObject explosion;
    [SerializeField]
    int explosionSize;

    AudioSource explosionSound;

    protected override void Awake()
    {
        base.Awake();

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        fuelSpendingTimer = 0;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        explosionSound = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        FuelManagement();
        if (fuel <= 0) rigidBody.useGravity = true;

        
    }

    void FixedUpdate()
    {
        Movement();
    }

    void FuelManagement()
    {
        if (fuel >= 0)
        {
            if (fuelSpendingTimer < fuelSpendingTime) fuelSpendingTimer += Time.deltaTime;
            else
            {
                fuel--;
                fuelSpendingTimer = 0;
            }
        }
        else
        {
            if (velocitySpendingTimer < velocitySpendingTime) velocitySpendingTimer += Time.deltaTime;
            else
            {
                velocity--;
                if (velocity < 0) velocity = 0;
                velocitySpendingTimer = 0;
            }
        }
    }

    void Movement()
    {
        Vector3 dir_ = direction.normalized;

        transform.position += direction * velocity * Time.deltaTime;
        transform.forward = direction;
    }
    /*

        */

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        hitEntity = collision.transform.gameObject.GetComponent<Entity>();

        Die(contact.point);    

    }


    protected override void Die(Vector3 contactPoint)
    {        
        GameObject e = Instantiate(explosion, contactPoint, Quaternion.identity);
        Explosion E = e.GetComponent<Explosion>();
        E.damage = damage;
        E.shooter = shooter;

        if (hitEntity != null)
        {
            damageEntity();
            E.AddHitEntity(hitEntity);
        }

        Debug.Log("gyro Explosions");
        explosionSound.Play();
        Destroy(gameObject);

    }

}
