using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysLaser : KineticProjectile
{
    public Vector3 start;
    Vector3 end;

    [SerializeField]
    private float size;
    private LineRenderer lr;    

    protected override void Awake()
    {
        base.Awake();

        start = transform.position - direction * size / 2;
        end = transform.position + direction * size / 2;

        lr = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialCalc();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        CheckColision();
        //Debug.Log(start);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        calculateLaser();

    }

    void LateUpdate()
    {
        CheckColision();

    }


    void calculateLaser()
    {
        //start += direction * velocity * Time.deltaTime;    
        //end += direction * velocity * Time.deltaTime;

        transform.position += direction * velocity * Time.deltaTime;
        start = transform.position - direction * size / 2;
        end = transform.position + direction * size / 2;  

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void CheckColision()
    {        
        RaycastHit hit;

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, direction, out hit, size / 2, layerMask))
        {
            if (hit.collider)
            {
                calcEntity(hit);
                Destroy(gameObject);
            }
        }
    }

    void calcEntity(RaycastHit hit)
    {
        hitEntity = hit.transform.gameObject.GetComponent<Entity>();
        if (hitEntity != null) hitEntity.ReceiveDamage(damage, shooter);

    }





}
