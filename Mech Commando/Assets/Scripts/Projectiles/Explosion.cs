using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    public float maxVisualSize;

    float maxColliderSize;
    [SerializeField]
    float speed;

    [SerializeField]
    public int maxDamage;
    int minDamage;

    ParticleSystem ps;
    SphereCollider coll;

    //Hit entity List so that entities aren't hit twice by the same explosion
    List<Entity> hitEntities;

    void Awake()
    {
        hitEntities = new List<Entity>();


    }

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        coll = GetComponent<SphereCollider>();

        ParticleSystem.SizeOverLifetimeModule sizeOL = ps.sizeOverLifetime;

        //Animation Curve with max Visual Size
        AnimationCurve curve = new AnimationCurve(); 
        curve.AddKey(0.0f, 0.0f);
        curve.AddKey(1f, maxVisualSize);

        sizeOL.size = new ParticleSystem.MinMaxCurve(1.5f, curve);
        //Initial Collider radius
        coll.radius = 0.1f; 

        //Max Collider Size based on the max Visual Size
        maxColliderSize = (maxVisualSize * 20) / 30;
       
        minDamage = (int)(maxDamage / maxVisualSize);

    }

    // Update is called once per frame
    void Update()
    {
        if (coll.radius <= maxColliderSize) //Increase Collider Size
            coll.radius += speed * Time.deltaTime;


        if (!ps.isEmitting) Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        Entity hitEntity = other.gameObject.GetComponent<Entity>();

        if (hitEntity != null) //if object hit is an entity
        {
            if (!hitEntities.Exists(x => x.GetInstanceID() == hitEntity.GetInstanceID())) //if it not already been hit and added to hitList
            {
                hitEntities.Add(hitEntity);

                int dmg = calcDamage(other.transform.position);

                hitEntity.ReceiveDamage(dmg);
            }


        }


    }

    private int calcDamage(Vector3 EntityPos) {

        int dmg = maxDamage;
        Vector3 closestPoint = coll.ClosestPointOnBounds(coll.center);
        float minDist = Vector3.Distance(coll.center, closestPoint);

        float dist = Vector3.Distance(transform.position, EntityPos);

        dmg = (int)(dist * minDamage / minDist);


        return dmg;

    }

    public void addHitEntity(Entity e)
    {
        hitEntities.Add(e);
    }
}
