using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinder
{


    protected class NodeRecordDij //The Culmination of Costs
    {
        public PFNode node;
        public Connection connection;
        public int costSoFar;

        public NodeRecordDij()
        {

        }

        public NodeRecordDij(int costSoFar, PFNode node, Connection connection)
        {
            this.node = node;
            this.connection = connection;
            this.costSoFar = costSoFar;
        }
    }

    protected class NodeRecondList : List<NodeRecordDij>
    {
        public NodeRecordDij Cheapest()
        {
            return this.OrderBy(record => record.costSoFar).First();
        }

        public NodeRecordDij Find(PFNode node)
        {
            NodeRecordDij rec = null;
            try
            {
                rec = Find(record => record.node == node);
            }
            catch (ArgumentNullException e)
            {
                //Just catch it
            }
            return rec;
        }
    }



    static public List<PFNode> PathFindDijkstra(Graph g, PFNode from, PFNode to)
    {
        NodeRecordDij current = null;

        NodeRecordDij startNodeRecord = new NodeRecordDij(0, from, null);

        NodeRecondList closed = new NodeRecondList();
        NodeRecondList open = new NodeRecondList();

        open.Add(startNodeRecord);

        while (open.Count > 0)
        {
            //get the most promising node
            current = open.Cheapest();

            if (current.node == to) break;

            foreach (var connection in g.GetConnections(current.node))
            {
                // look to specific neighbou at a time
                PFNode endNode = connection.ToNode;
                //compute endNodeCost
                int endNodeCost = current.costSoFar + connection.Cost;

                // did we visit this neighbour before?
                NodeRecordDij endNodeRecord = closed.Find(endNode);
                // yes, we did
                if (endNodeRecord != null) continue;

                //did we put this neighbour in the open list already?
                endNodeRecord = open.Find(endNode);
                //yes, this neighbour is already in the open list
                if (endNodeRecord != null)
                {
                    // out path is more expensive
                    if (endNodeCost > endNodeRecord.costSoFar) continue;
                }
                else
                {
                    endNodeRecord = new NodeRecordDij();

                    endNodeRecord.node = endNode;

                    open.Add(endNodeRecord);

                }
                //Update the connection we came from
                endNodeRecord.connection = connection;
                //update the cost for this node
                endNodeRecord.costSoFar = endNodeCost;
            }
            //we are done visiting all our neighbours
            //mark this done as processed,closing it
            open.Remove(current);
            closed.Add(current);
        }

        //we got here with a path if current is the end node
        if (current != null && current.node != to) return null;

        //create the path, for now empty
        List<PFNode> path = new List<PFNode>();

        //add the last node
        path.Add(to);

        //while we are not in the first node of the path;
        while (current.connection != null)
        {
            //get the source node of the connection
            PFNode fromNode = current.connection.FromNode;
            //add it to the path
            path.Add(fromNode);
            //use it to find the previous node record in the closed list
            current = closed.Find(fromNode);
        }


        //reverse the list
        path.Reverse();
        return path;
    }


    protected class NodeRecordA //The Culmination of Costs
    {
        public PFNode node;
        public Connection connection;
        public int costSoFar;
        public int estimatedTotalCost;

        public NodeRecordA()
        {

        }

        public NodeRecordA(int costSoFar, PFNode node, Connection connection, int estimated)
        {
            this.node = node;
            this.connection = connection;
            this.costSoFar = costSoFar;
            this.estimatedTotalCost = estimated;
        }
    }


    protected class NodeRecondListA : List<NodeRecordA>
    {
        public NodeRecordA Cheapest()
        {
            return this.OrderBy(record => record.costSoFar).First();
        }

        public NodeRecordA Find(PFNode node)
        {
            NodeRecordA rec = null;
            try
            {
                rec = Find(record => record.node == node);
            }
            catch (ArgumentNullException e)
            {
                //Just catch it
            }
            return rec;
        }
    }


    public  class Heuristic
    {
        PFNode from;

        public Heuristic(PFNode n)
        {
            this.from = n;
        }

        public  float EuclidianDistance(PFNode start, PFNode end)
        {
            float dx = Math.Abs(start.transform.position.x - end.transform.position.x);
            float dy = Math.Abs(start.transform.position.y - end.transform.position.y);
            float dz = Math.Abs(start.transform.position.z - end.transform.position.z);
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public float Estimate(PFNode toNode)
        {
            float dx = Math.Abs(from.transform.position.x - toNode.transform.position.x);
            float dy = Math.Abs(from.transform.position.y - toNode.transform.position.y);
            float dz = Math.Abs(from.transform.position.z - toNode.transform.position.z);
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }

    static public List<PFNode> PathFindAstar(Graph g, PFNode from, PFNode to)
    {
        Heuristic heuristic = new Heuristic(from);

        NodeRecordA current = null;
        NodeRecordA startRecord = new NodeRecordA(0, from, null, (int)heuristic.Estimate(to));

        NodeRecondListA closed = new NodeRecondListA();
        NodeRecondListA open = new NodeRecondListA();

        open.Add(startRecord);

        while(open.Count > 0)
        {
            current = open.Cheapest();

            if (current.node == to) break;

            List<Connection> connections = g.GetConnections(current.node);

            foreach (Connection c in connections)
            {
                PFNode endNode = c.ToNode;

                int endNodeCost = current.costSoFar + c.Cost;
                int endNodeHeuristic = 0;
                NodeRecordA endNodeRecord = closed.Find(endNode);

                if (endNodeRecord != null)
                {
                    if (endNodeRecord.costSoFar <= endNodeCost) continue;
                    closed.Remove(endNodeRecord);
                    open.Add(endNodeRecord);

                    endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                }
                else
                {
                    endNodeRecord = open.Find(endNode);
                    if (endNodeRecord != null)
                    {
                        if (endNodeRecord.costSoFar <= endNodeCost) continue;

                        endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                    }
                    else
                    {
                        endNodeRecord = new NodeRecordA();
                        endNodeRecord.node = endNode;
                        endNodeHeuristic = (int)heuristic.Estimate(endNode); 
                        open.Add(endNodeRecord);
                    }
                }

                endNodeRecord.costSoFar = endNodeCost;

                endNodeRecord.connection = c;

                endNodeRecord.estimatedTotalCost = endNodeCost - endNodeHeuristic;
            }
            open.Remove(current);
            closed.Add(current);          

        }

        if (current.node != to) return null;

        List<PFNode> path = new List<PFNode>();
        while (current.node != from)
        {
            path.Add(current.connection.ToNode);
            current = closed.Find(current.connection.FromNode);
        }

        path.Add(from);
        path.Reverse();

        return path;
    }




    }
