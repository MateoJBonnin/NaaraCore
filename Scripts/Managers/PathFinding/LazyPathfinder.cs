using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LazyPathfinder<Node> : IEnumerator<Node> where Node : class
{
    private List<PathfinderState<Node>> pathFinderStates;
    private int currentStateIndex;
    private Func<Node, bool> reachedGoalPredicate;
    private Action<List<Node>> onGoalReachedAction;
    private Func<Node, Node, float> heuristicFunction;
    private Func<Node, List<Connection<Node>>> expandFunction;
    private Func<Node, Node, bool> thetaPredicate;

    public LazyPathfinder(Func<Node, bool> reachedGoalPredicate, Action<List<Node>> onGoalReachedAction, Func<Node, Node, float> heuristicFunction, Func<Node, List<Connection<Node>>> expandFunction, Func<Node, Node, bool> thetaPredicate, Node initNode, Node goalNode)
    {
        this.pathFinderStates = new List<PathfinderState<Node>>();
        this.reachedGoalPredicate = reachedGoalPredicate;
        this.onGoalReachedAction = onGoalReachedAction;
        this.heuristicFunction = heuristicFunction;
        this.expandFunction = expandFunction;
        this.thetaPredicate = thetaPredicate;
        this.currentStateIndex = 0;
        this.SetInitialState(initNode, goalNode);
    }

    public Node Current => pathFinderStates[this.currentStateIndex].currentNode;

    object IEnumerator.Current => this.Current;

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        this.pathFinderStates.Add(ProcessNextState(this.pathFinderStates[this.currentStateIndex]));
        this.currentStateIndex++;
        return pathFinderStates[this.currentStateIndex].openNodes.Count > 0;
    }

    public void Reset()
    {
        this.pathFinderStates.Clear();
        this.currentStateIndex = 0;
    }

    public PathfinderState<Node> ProcessNextState(PathfinderState<Node> pathfinderState)
    {
        Node current = pathfinderState.openNodes.First();
        pathfinderState.openNodes.Remove(current);
        pathfinderState.closeNodes.Add(current);

        if (this.reachedGoalPredicate(current))
            this.onGoalReachedAction(this.ReconstructPath(new PathfinderState<Node>(pathfinderState, current)));

        foreach (Connection<Node> cnn in this.expandFunction(current))
        {
            if (pathfinderState.closeNodes.Contains(cnn.connectedNode))
                continue;

            float tempGValue = pathfinderState.gValues[current] + cnn.cost;
            if (tempGValue > pathfinderState.gValues.DefaultGet(cnn.connectedNode, () => tempGValue))
                continue;

            pathfinderState.parents[cnn.connectedNode] = current;
            pathfinderState.gValues[cnn.connectedNode] = tempGValue;
            pathfinderState.fValues[cnn.connectedNode] = tempGValue + this.heuristicFunction(cnn.connectedNode, pathfinderState.goalNode);
            pathfinderState.openNodes.Add(cnn.connectedNode);
        }

        return new PathfinderState<Node>(pathfinderState, current);
    }

    private void SetInitialState(Node initNode, Node goalNode)
    {
        Dictionary<Node, float> initialFValues = new Dictionary<Node, float>();
        Dictionary<Node, float> initialGValues = new Dictionary<Node, float>();
        SortedSet<Node> initialOpenNodes = new SortedSet<Node>(new Pathfinder<Node>.FValueComparer(initialFValues));
        HashSet<Node> initialCloseNodes = new HashSet<Node>();

        initialGValues[initNode] = 0;
        initialFValues[initNode] = this.heuristicFunction(initNode, goalNode);
        initialOpenNodes.Add(initNode);

        PathfinderState<Node> initialState = new PathfinderState<Node>(
            initialGValues,
            initialFValues,
            initialOpenNodes,
            initialCloseNodes,
            new Dictionary<Node, Node>(),
            goalNode,
            initNode,
            initNode);

        this.pathFinderStates.Add(initialState);
    }

    private List<Node> ReconstructPath(PathfinderState<Node> state)
    {
        List<Node> pathList = new List<Node>();
        var current = state.currentNode;

        while (current != null)
        {
            state.parents.TryGetValue(current, out Node value);

            if (value != null && (current == state.goalNode || this.thetaPredicate(current, value)))
                pathList.Add(current);

            current = value;
        }

        pathList.Reverse();
        return pathList;
    }
}

public class PathfinderState<Node> where Node : class
{
    public PathfinderState(Dictionary<Node, float> gValues, Dictionary<Node, float> fValues, SortedSet<Node> openNodes, HashSet<Node> closeNodes, Dictionary<Node, Node> parents, Node goalNode, Node initNode, Node currentNode)
    {
        this.parents = parents;
        this.gValues = gValues;
        this.fValues = fValues;
        this.openNodes = openNodes;
        this.closeNodes = closeNodes;
        this.goalNode = goalNode;
        this.initNode = initNode;
        this.currentNode = currentNode;
    }

    public PathfinderState(PathfinderState<Node> pathfinderState, Node currentNode)
    {
        this.parents = pathfinderState.parents;
        this.gValues = pathfinderState.gValues;
        this.fValues = pathfinderState.fValues;
        this.openNodes = pathfinderState.openNodes;
        this.closeNodes = pathfinderState.closeNodes;
        this.goalNode = pathfinderState.goalNode;
        this.initNode = pathfinderState.initNode;
        this.currentNode = currentNode;
    }

    public SortedSet<Node> openNodes;
    public HashSet<Node> closeNodes;
    public Dictionary<Node, float> gValues;
    public Dictionary<Node, float> fValues;
    public Dictionary<Node, Node> parents;
    public Node goalNode;
    public Node initNode;
    public Node currentNode;
}