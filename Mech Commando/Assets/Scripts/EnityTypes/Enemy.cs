using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity
{
    [SerializeField]
    protected int damageOutput;
    [SerializeField]
    protected int accuracy;

    [SerializeField]
    protected float radarRange;

    protected bool InRange;

    //AI
    protected AIMovementManager movementManager;
    protected EnemyManager manager;
    protected MovementInfo currentTarget;
    protected MovementInfo player;

    [SerializeField]
    protected List<PFNode> currentPath;

    protected override void Awake()
    {
        base.Awake();

        currentPath = new List<PFNode>();
        movementManager = GetComponent<AIMovementManager>();
        EnemyManager.SubcribeSlaves += SubcribeToManager;

        currentTarget = player;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        movementManager.Run(currentTarget, info, speed);
    }

    public virtual void SubcribeToManager(EnemyManager manager) {
        this.manager = manager;
        manager.Enemies.Add(this);

        player = manager.getPlayer().GetInfo;
    }    

    public override void Die()
    {      

        if (manager == null)
        {
            Debug.Log("this Enemy wasn't associated to any manager");
        }
        else
        {
            manager.Enemies.Remove(this);
        }
        base.Die();
    }
    public EnemyManager getManager() => manager;

    public void ClearCurrentPath() => currentPath.Clear();

    protected virtual bool isPathObstructed(Vector3 from, MovementInfo target)
    {
        bool isObstructed = true;
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = ~(1 << 8); //& 1 << 12 & 1 << 13)

        Vector3 dir = target.position - from;
        float distance = Vector3.Distance(target.position, from);

        RaycastHit hit;
        if (Physics.Raycast(from, dir, out hit, distance, layerMask))
        {
            Debug.DrawRay(from, dir * distance, Color.green, 0.1f,false);
            if (hit.collider.gameObject.GetComponent<Player>() != null) isObstructed = false;
            else isObstructed = true;
        }
        else
        {    
            Debug.DrawRay(from, dir * distance, Color.red, 0.1f, false);
            isObstructed = false;
        }
        return isObstructed;
    }

    public virtual void GetNextPathTarget()
    {
        if (currentPath.Count > 1)
        {
            PFNode oldTarget = currentPath[0];
            currentTarget = currentPath[1].GetInfo;
            currentPath.Remove(oldTarget);
        }
        else if (currentPath.Count == 1)
        {
            currentTarget = manager.getPlayer().GetInfo;
            currentPath.Clear();
        } else
        {
            //do nothing
        }
    }

    public PFNode GetCurrentTarget()
    {
        if (currentPath.Count > 0)
        {
            return currentPath[0];
        }
        else return null;
    }

}
