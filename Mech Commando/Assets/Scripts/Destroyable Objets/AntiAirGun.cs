using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirGun : StaticEntity
{
    [SerializeField]
    GameObject destroyedState;

    [SerializeField]
    GameObject Explosion;

    LocalObjectiveManager manager;
    public string location;
    Transform explosionSpawn;

    protected override void Awake()
    {
        LocalObjectiveManager.SubcribeSlaves += SubcribeToManager;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        explosionSpawn = transform.Find("Top");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void ReceiveDamage(int damage, Entity shooter)
    {
        if (shooter is Player)
        base.ReceiveDamage(damage, shooter);



       // Debug.Log("");
    }

    public override void Die()
    {
        base.Die();
        manager.Enemies.Remove(this);
        Instantiate(Explosion, explosionSpawn.position, Quaternion.identity);
        Instantiate(destroyedState, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    protected virtual void SubcribeToManager(LocalObjectiveManager manager, string location)
    {
        if (this.location == location)
        {
            this.manager = manager;
            manager.Enemies.Add(this);
        }
    }
}
