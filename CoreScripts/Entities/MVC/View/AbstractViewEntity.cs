using System;
using System.Collections.Generic;
using Pool;
using System.Linq;
using UnityEngine;

public abstract class AbstractViewEntity : MonoBehaviour, IPooleable, INavTargeteable
{
    public abstract void ProcessEntityAction(ActionRequestType actionRequestType);

    public CharacterType Type { get; set; }
    public Rigidbody rb;
    public event Action<IPooleable> OnReturnedItem;

    [SerializeField]
    private float closestNodesRadius;

    public void EnableObject()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableObject()
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

    public virtual void Reset()
    {
        this.OnReturnedItem?.Invoke(this);
    }
}
