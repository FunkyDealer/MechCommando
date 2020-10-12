using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : HitScanProjectile
{
    [SerializeField]
    private int maxSize;
    private LineRenderer lr;

    [SerializeField]
    private float lifeTime;
    private float lifeTimer;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        calculateLaser();
        lifeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (lifeTimer < lifeTime) lifeTimer += Time.deltaTime;
        else Destroy(gameObject);



    }

    void LateUpdate()
    {
        start += direction * speed * Time.deltaTime;
        lr.SetPosition(0, start);


    }


    void calculateLaser()
    {
        lr.SetPosition(0, start);
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, (direction * maxSize).magnitude))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }

        }
        else
        {
            Debug.Log($"End Position: {direction * 10}");
            lr.SetPosition(1,start + direction * maxSize);
        }


    }

}
