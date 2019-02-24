using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGridPopulatorTechnic<T>
{
    public abstract List<INodeable> PopulateGrid(Func<INodeable> getNode);
    public abstract void ConfigureNodes(List<T> nodesToConfigure);
}