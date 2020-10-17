using System.Collections;
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

        transform.right = direction;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement();

        if (fuel <= 0) rigidBody.useGravity = true;

    }

    void movement()
    {
        Vector3 dir_ = direction.normalized;


        transform.position += direction * velocity * Time.deltaTime;
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
    /*

        */

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        // Instantiate(explosionPrefab, position, rotation);

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
