using Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewEntity : MonoBehaviour, IPooleable, INavTargeteable
{
    public ViewEntityBlackboard EntityBlackboard { get; set; }

    public abstract void ProcessEntityAction(ActionRequestType actionRequestType);

    public Rigidbody rb;
    public event Action<IPooleable> OnReturnedItem;

    public virtual void EnableObject()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public List<INavTargeteable> GetClosests()
    {
        //when someone storages all the nav targeteabbles, ask him for the closets ones.
        return new List<INavTargeteable>();
        // return FindObjectsOfType<MonoBehaviour>().OfType<INavTargeteable>().Where(node => Vector3.Distance(node.GetPosition(), this.transform.position) <= this.closestNodesRadius).ToList();
    }

    public void FixedUpdate()
    {
        this.EntityBlackboard.FixedUpdateBlackboard();
    }

    public virtual void Reset()
    {
        this.OnReturnedItem?.Invoke(this);
    }
}