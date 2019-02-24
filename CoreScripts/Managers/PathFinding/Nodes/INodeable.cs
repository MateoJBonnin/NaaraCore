using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INodeable
{
    List<INodeable> NeighNodes { get; set; }
    List<INodeable> GetNeighNodes();
}
