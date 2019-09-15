using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderManager<T> where T : class
{
    private Pathfinder<T> pathfinder;
    private LazyPathfinder<T> lazyPathfinder;

    public PathfinderManager()
    {
        this.pathfinder = new Pathfinder<T>();
    }

    public Tuple<List<T>, Dictionary<T, T>> GetAstarPathfind(
        T initialNode,
        T goalNode,
        Func<T, bool> predicate,
        Func<T, T, float> heuristic,
        Func<T, List<Connection<T>>> expand)
    {
        return this.pathfinder.FindPath(initialNode, goalNode, predicate, heuristic, expand);
    }

    public List<T> GetThetaPathfind(
        T initialNode,
        T goalNode,
        Func<T, bool> predicate,
        Func<T, T, float> heuristic,
        Func<T, List<Connection<T>>> expand,
        Func<List<T>, List<T>> thetaPredicate)
    {
        return thetaPredicate(this.GetAstarPathfind(initialNode, goalNode, predicate, heuristic, expand).Item1);
    }

    public IEnumerator<float> GetLazyAstarPathfind(T initialNode,
        T goalNode,
        Func<T, bool> breakPredicate,
        Func<T, T, float> heuristic,
        Func<T, List<Connection<T>>> expand,
        Func<T, T, bool> thetaPredicate,
        Action<List<T>> onCompleted,
        float stepsAmountByFrame)
    {
        int stepCount = 0;
        this.lazyPathfinder = new LazyPathfinder<T>(breakPredicate, onCompleted, heuristic, expand, thetaPredicate, initialNode, goalNode);
        while (this.lazyPathfinder.MoveNext() && !breakPredicate(this.lazyPathfinder.Current))
        {
            stepCount++;
            //IF I YIELD THIS THE PATHFINDER TAKES AS 10 MORE TIME.
            //BUT ANYWAYS I DONT USE THIS FOR NOW.
            //yield return this.lazyPathfinder.Current;
            if (stepCount >= stepsAmountByFrame)
            {
                yield return Timing.WaitForOneFrame;
                stepCount = 0;
            }
        }
    }

    public float GetManhattanDistanceHeuristic(INavTargeteable currentNode, INavTargeteable goalNode)
    {
        var currPos = currentNode.GetPosition();
        var goalPos = goalNode.GetPosition();

        var distanceX = Mathf.Abs(currPos.x - goalPos.x);
        var distanceZ = Mathf.Abs(currPos.z - goalPos.z);

        return (distanceX + distanceZ);
    }

    public float GetEuclideanDistanceHeuristic(INavTargeteable currentNode, INavTargeteable goalNode)
    {
        return Vector3.SqrMagnitude(goalNode.GetPosition() - currentNode.GetPosition());
    }
}
