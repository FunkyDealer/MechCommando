using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public List<Enemy> Enemies;
    [SerializeField]
    private Player thePlayer;


    public delegate void SubscriptionHandler(EnemyManager manager);
    public static event SubscriptionHandler SubcribeSlaves;


    public List<PFNode> pathFindingNodes;

    Graph graph;


    void Awake()
    {
        Enemies = new List<Enemy>();
        thePlayer = FindObjectOfType<Player>();

    }


    // Start is called before the first frame update
    void Start()
    {
        SubcribeSlaves(this);

        graph = new Graph(pathFindingNodes);

        //List<PFNode> path = PathFinder.PathFindAstar(graph, nodeStart, nodeEnd);
        //foreach (PFNode node in path)
        //{
        //    Debug.Log(node.gameObject.name);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player getPlayer() => thePlayer;

    

    public List<PFNode> getPathToTarget(MovementInfo npc, MovementInfo target)
    {
        Enemy e = (Enemy)npc.Object;
        e.ClearCurrentPath();

        PFNode closestToNPC = ClosestNode(npc);
        PFNode closestToTarget = ClosestNode(target);

        List<PFNode> path = PathFinder.PathFindAstar(graph, closestToNPC, closestToTarget);

        return path;
    }

    PFNode ClosestNode(MovementInfo actor)
    {
        PFNode closestNode = null;
        Vector3 actorPos = actor.position;

        float currentMinDistance = 9999999;

        foreach (var n in pathFindingNodes)
        {
            float distance = Vector3.Distance(actorPos, n.transform.position);
            if (distance < currentMinDistance) { currentMinDistance = distance; closestNode = n; };
        }

        if (closestNode == null) throw new System.Exception($"CLOSEST NODE WAS NULL");
        else
        {
            Debug.Log($"closest node is {closestNode.gameObject.name}");
        }
        return closestNode;
    }
}
