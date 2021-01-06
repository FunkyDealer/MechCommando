using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{    
    public PFNode StartNode;
    public PFNode EndNode;

    EnemyManager manager;

    void Awake()
    {
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

    protected virtual void SubcribeToManager(EnemyManager manager)
    {
        this.manager = manager;
        manager.Patrols.Add(this);
    }

    public MovementInfo furthestNode(MovementInfo actor)
    {
        float distanceStart = Vector3.Distance(actor.position, StartNode.GetInfo.position);
        float distanceEnd = Vector3.Distance(actor.position, EndNode.GetInfo.position);

        if (distanceStart > distanceEnd) return StartNode.GetInfo;
        else return EndNode.GetInfo;
    } 
}
