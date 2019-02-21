using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTreeQuestionNode<T> : GameTreeNode<T>
{
    private GameTreeNode<T> trueNode;
    private GameTreeNode<T> falseNode;
    private Func<bool> predicate;

    public GameTreeQuestionNode(GameTreeNode<T> trueNode, GameTreeNode<T> falseNode, Func<bool> predicate)
    {
        this.trueNode = trueNode;
        this.falseNode = falseNode;
        this.predicate = predicate;
    }

    public override T Execute(T data)
    {
        if (predicate())
            return this.trueNode.Execute(data);
        else
            return this.falseNode.Execute(data);
    }
}