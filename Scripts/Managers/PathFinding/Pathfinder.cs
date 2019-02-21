using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder<Node> where Node : class
{
    private SortedSet<Node> openNodes;
    private HashSet<Node> closeNodes;
    private Dictionary<Node, float> gValues;
    private Dictionary<Node, float> fValues;
    private Dictionary<Node, Node> parents;
    private HashSet<Node> nodePath;

    public Tuple<List<Node>, Dictionary<Node, Node>> FindPath(
        Node initialNode,
        Node goalNode,
        Func<Node, bool> predicate,
        Func<Node, Node, float> heuristic,
        Func<Node, IEnumerable<Connection<Node>>> expand)
    {
        this.parents = new Dictionary<Node, Node>();
        this.gValues = new Dictionary<Node, float>();
        this.fValues = new Dictionary<Node, float>();
        this.openNodes = new SortedSet<Node>(new FValueComparer(this.fValues));
        this.closeNodes = new HashSet<Node>();

        this.gValues[initialNode] = 0;
        this.fValues[initialNode] = heuristic(initialNode, goalNode);

        this.openNodes.Add(initialNode);

        while (openNodes.Count > 0)
        {
            Node current = openNodes.First();
            this.openNodes.Remove(current);
            this.closeNodes.Add(current);

            if (predicate(current))
            {
                //Debug.Log("Predicate is true " + current + " " + this.nodePath.Count);
                return new Tuple<List<Node>, Dictionary<Node, Node>>(this.ReconstructPath(current, initialNode), this.parents);
            }

            foreach (var cnn in expand(current))
            {
                if (this.closeNodes.Contains(cnn.connectedNode))
                    continue;

                float tempGValue = this.gValues[current] + cnn.cost;
                if (tempGValue > this.gValues.DefaultGet(cnn.connectedNode, () => tempGValue))
                    continue;

                this.parents[cnn.connectedNode] = current;
                this.gValues[cnn.connectedNode] = tempGValue;
                this.fValues[cnn.connectedNode] = tempGValue + heuristic(cnn.connectedNode, goalNode);

                this.openNodes.Add(cnn.connectedNode);
            }
        }

        return new Tuple<List<Node>, Dictionary<Node, Node>>(nodePath.ToList(), this.parents);
    }

    private List<Node> ReconstructPath(Node node, Node initialNode)
    {
        List<Node> pathList = new List<Node>();
        var current = node;
        while (current != null)
        {
            pathList.Add(current);
            this.parents.TryGetValue(current, out Node value);
            current = value;
        }
        pathList.Reverse();
        return pathList;
    }

    public class FValueComparer : IComparer<Node>
    {
        private Dictionary<Node, float> fValues;
        public FValueComparer(Dictionary<Node, float> fValues)
        {
            this.fValues = fValues;
        }

        public int Compare(Node xNode, Node yNode)
        {
            return this.fValues[xNode].CompareTo(this.fValues[yNode]);
        }
    }
}

public class Connection<Node>
{
    public Node connectedNode;
    public float cost;

    public Connection(Node endPoint, float cost)
    {
        this.connectedNode = endPoint;
        this.cost = cost;
    }
}