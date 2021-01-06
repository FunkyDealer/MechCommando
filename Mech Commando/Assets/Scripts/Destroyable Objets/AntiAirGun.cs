using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirGun : StaticEntity
{
    [SerializeField]
    GameObject destroyedState;

    LocalObjectiveManager manager;
    public string location;

    protected override void Awake()
    {
        LocalObjectiveManager.SubcribeSlaves += SubcribeToManager;
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
