using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGridPopulatorTechnic
{
    public abstract List<INodeable> PopulateGrid(Func<INodeable> getNode);
    public abstract void ConfigureNodes();
}
