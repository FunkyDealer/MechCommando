using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    private PFNode fromNode;
    private PFNode toNode;
    private int cost;

    public PFNode FromNode => fromNode;
    public PFNode ToNode => toNode;
    public int Cost => cost;

    public Connection(PFNode from, PFNode to, int cost)
    {
        fromNode = from;
        toNode = to;
        this.cost = cost;
    }

    int calculateCost(PFNode from, PFNode to)
    {
        return from.hConnections[to];
    }


}
