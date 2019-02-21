using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pool;
using System;
using Managers;

public class PathNode : MonoBehaviour, INodeable, IPooleable, INavTargeteable
{
    public float connectionsRadius;

    public List<INodeable> NeighNodes { get; set; }

    public event Action<IPooleable> OnReturnedItem;

    //public List<INodeable> ComputeConnectionNodes()
    //{
    //    List<PathNode> allNodes = MapManager.Instance.GetPathNodes();
    //    allNodes = allNodes
    //        .Where(node => Vector3.SqrMagnitude(node.transform.position - this.transform.position) <= connectionsRadius)
    //        .Where(node => node != this)
    //        .ToList();
    //    this.nearNodes = allNodes;
    //    return this.nearNodes.Select(node => (INodeable)node).ToList();
    //}

    public void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableObject()
    {
        this.gameObject.SetActive(true);
    }

    public virtual List<INavTargeteable> GetClosests()
    {
        return this.NeighNodes.Select(node => (INavTargeteable)node).ToList();
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}