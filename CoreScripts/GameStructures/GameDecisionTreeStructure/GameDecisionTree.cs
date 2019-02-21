using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDecisionTree<T>
{
    public GameTreeNode<T> seed;

    public GameDecisionTree(GameTreeNode<T> seed)
    {
        this.seed = seed;
    }
}