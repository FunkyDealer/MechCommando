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
        lr = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        initialCalc();
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


    void initialCalc()
    {

        end = start + direction * size;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        
        CheckColision();
    }

    void calculateLaser()
    {
        start += direction * velocity * Time.deltaTime;      

        end += direction * velocity * Time.deltaTime;

       
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void CheckColision()
    {
        float distance = Vector3.Distance(start, end);

        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, distance))
        {
            if (hit.collider)
            {
                calcEntity(hit);                
            }
        }
    }

    void calcEntity(RaycastHit hit)
    {
        hitEntity = hit.transform.gameObject.GetComponent<Entity>();
        if (hitEntity != null) { damageEntity(); Debug.Log("entered"); }
        Destroy(gameObject);
    }
}
