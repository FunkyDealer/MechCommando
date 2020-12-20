using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFNode : MonoBehaviour
{
    [SerializeField]
    List<PFNode> connections;

    [HideInInspector]
    public Dictionary<PFNode,int> hConnections;

    EnemyManager manager;

    
    MovementInfo info;
    public MovementInfo GetInfo => info;

    void Awake()
    {
        generateConnectionsCost();
        EnemyManager.SubcribeSlaves += SubcribeToManager;
        info = new MovementInfo();
        info.position = transform.position;
        info.velocity = Vector3.zero;
        Vector3 forward = transform.forward;
        info.orientation = Mathf.Atan2(forward.x, forward.z);
        info.Object = this;
    }


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void generateConnectionsCost()
    {
        hConnections = new Dictionary<PFNode, int>();
        foreach (var c in connections)
        {
            int distance2otherNode = (int)Vector3.Distance(gameObject.transform.position, c.gameObject.transform.position);

            hConnections.Add(c, distance2otherNode);
        }
    }

    protected virtual void SubcribeToManager(EnemyManager manager)
    {
        this.manager = manager;
        manager.pathFindingNodes.Add(this);
    }

    public EnemyManager getManager() => manager;


    private void OnTriggerEnter(Collider other)
    {
        Enemy e = other.gameObject.GetComponent<Enemy>();
        Debug.Log("NPC entered the Node");
        if (e != null)
        {
            
            e.GetNextPathTarget();
        }
        

    }
}
