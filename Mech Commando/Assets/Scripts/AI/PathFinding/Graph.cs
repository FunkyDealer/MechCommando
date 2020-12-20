using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    Dictionary<PFNode, List<Connection>> connections;

    public Graph(List<PFNode> nodeList)
    {
        connections = new Dictionary<PFNode, List<Connection>>();

        foreach (var n in nodeList)
        {
            AddConnection(n);
        }
    }

    public List<Connection> GetConnections(PFNode node)
    {
        //if (connections.ContainsKey(node)) return connections[node];
        //return null;
        return connections?[node];
    }


    public void AddConnection(PFNode node)
    {
       // if (node.hConnections.Count > 0) Debug.Log($"node {node.gameObject.name} has {node.hConnections.Count} connections");

        foreach (var con in node.hConnections) //Foreach connection in the node
        {
            Connection c = new Connection(node, con.Key, con.Value); //Create a new Connection for the graph' connection map
            if (!connections.ContainsKey(node)) //if the connection list doesn't exist
                connections[node] = new List<Connection>(); //create it
            connections[node].Add(new Connection(node, con.Key, con.Value));
        }

    }




}
