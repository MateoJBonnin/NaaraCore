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

    public List<INodeable> GetNeighNodes()
    {
        List<INodeable> neighNodes = new List<INodeable>(ManagersService.instace.GetManager<GameMap>().mapManager.GetPathNodes());
        neighNodes = neighNodes
             .Where(n => Vector3.SqrMagnitude(((PathNode)n).transform.position - transform.position) <= connectionsRadius)
             .Where(n => ((PathNode)n) != this)
             .ToList();

        return neighNodes;
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}