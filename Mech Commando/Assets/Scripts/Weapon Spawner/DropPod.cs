using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPod : MonoBehaviour
{
    [HideInInspector]
    public GameObject weapon;
    Transform spawnPos;
    float distance2Ground;
    [SerializeField]
    float speed;

    [SerializeField]
    float Distance2Stop;
    [SerializeField]
    float Distance2SlowDown;

    [SerializeField]
    float slowDownSpeed;
    bool fallen;

    float timer2Despawn;
    [SerializeField]
    float time2Despawn;
    bool spawnedW; //has the weapon spawned?

    void Awake()
    {
        fallen = false;
        spawnedW = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.Find("Spawn");
        calcDistance();;
        timer2Despawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (distance2Ground > Distance2Stop)
        {
            calcDistance();
            Movement();           
        }
        else
        {
            fallen = true;
            if (timer2Despawn < time2Despawn) timer2Despawn += Time.deltaTime;
            else Die();
        }

        if (fallen)
        {
            if (!spawnedW) SpawnWeapon();
        }
        
    }

    void FixedUpdate()
    {
       

        
    }

    private void Movement()
    {
        if (distance2Ground <= Distance2SlowDown)
        {
            if (speed > slowDownSpeed) speed -= 100 * Time.deltaTime;
            Debug.Log(speed);
        }
        transform.position += Physics.gravity * speed * Time.deltaTime;

       // Debug.Log(distance2Ground);
    }

    float calcDistance()
    {
        // Bit shift the index of the layer (10) to get a bit mask
        int layerMask = 1 << 10;
        // This cast rays only against colliders in layer 10. (The Ground)
        RaycastHit hit;
        // Does the ray intersect any objects excluding the Ground layer
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 999999, layerMask))
        {           
            distance2Ground = Vector3.Distance(transform.position, hit.point);
        }
        else
        {
            distance2Ground = 0;
        }

        return distance2Ground;
    }

    void SpawnWeapon()
    {
      //  spawnPos = transform.Find("Spawn");
        Instantiate(weapon, spawnPos.position, Quaternion.identity);

        spawnedW = true;
    }

   void Die()
    {
        Destroy(gameObject);
    }

}
