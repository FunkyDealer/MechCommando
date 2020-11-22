using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity
{
    [SerializeField]
    protected int damageOutput;
    [SerializeField]
    protected int movementType;
    [SerializeField]
    protected int accuracy;

    AIMovementManager movementManager;
    EnemyManager manager;

    protected override void Awake()
    {
        base.Awake();

        movementManager = GetComponent<AIMovementManager>();
        EnemyManager.SubcribeSlaves += SubcribeToManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected virtual void SubcribeToManager(EnemyManager manager) {

        this.manager = manager;
        manager.Enemies.Add(this);
    }

    public override void Die()
    {
        base.Die();
        manager.Enemies.Remove(this);
    }


}
