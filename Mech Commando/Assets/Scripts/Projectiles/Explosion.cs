using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SphereCollider coll;

    [SerializeField]
    public int damage;

    //Hit entity List so that entities aren't hit twice by the same explosion
    List<Entity> hitEntities;

    public Entity shooter;

    void Awake()
    {
        hitEntities = new List<Entity>();
        coll = GetComponent<SphereCollider>();
        
    }

    // Start is called before the first frame update
    void Start()
    {

        damage = (int)(damage / 3);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity hitEntity = other.gameObject.GetComponent<Entity>();
        if (hitEntity != null) //if object hit is an entity
        {
            if (!hitEntities.Exists(x => x.GetInstanceID() == hitEntity.GetInstanceID())) //if it not already been hit and added to hitList
            {
                hitEntities.Add(hitEntity);

                hitEntity.ReceiveDamage(damage, shooter);
            }
        }
    }

    public void AddHitEntity(Entity e)
    {
        hitEntities.Add(e);
    } 

}
