using System;
using UnityEngine;

[Serializable]
public class RigidbodyStateSnapshot : StateSnapshot
{
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public RigidbodyStateSnapshot()
    {
    }

    public RigidbodyStateSnapshot(Vector3 velocity, Vector3 angularVelocity)
    {
        this.velocity = velocity;
        this.angularVelocity = angularVelocity;
    }

    public RigidbodyStateSnapshot(Rigidbody rb)
    {
        this.velocity = rb.velocity;
        this.angularVelocity = rb.angularVelocity;
    }
}