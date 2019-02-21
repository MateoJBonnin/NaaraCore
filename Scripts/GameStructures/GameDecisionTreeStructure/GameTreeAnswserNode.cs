using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTreeAnswerNode<T> : GameTreeNode<T>
{
    private T item;

    public GameTreeAnswerNode(T item)
    {
        this.item = item;
    }

    public override T Execute(T data)
    {
        return item;
    }
}