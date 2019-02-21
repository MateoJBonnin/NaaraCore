using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTreeNode<T>
{
    public virtual T Execute(T data)
    {
        return data;
    }
}