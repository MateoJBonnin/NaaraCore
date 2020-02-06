using Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        List<PathNode> neighNodes = GameObject.FindObjectsOfType<PathNode>().ToList();//new List<INodeable>(GameplayController.instance.GamePlayer.subManagerSystem.GetManager<GameMap>().mapManager.GetPathNodes());
        neighNodes = neighNodes
             .Where(n => Vector3.SqrMagnitude(n.transform.position - transform.position) <= connectionsRadius)
             .Where(n => (n) != this)
             .ToList();

        return neighNodes.Select(nodes => nodes as INodeable).ToList();
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}