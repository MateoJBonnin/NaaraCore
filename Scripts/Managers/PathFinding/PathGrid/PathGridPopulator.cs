using System;
using System.Collections.Generic;
using System.Linq;

public class PathGridPopulator
{
    public AbstractGridPopulatorTechnic abstractGridPopulator;

    public PathGridPopulator(AbstractGridPopulatorTechnic abstractGridPopulator)
    {
        this.abstractGridPopulator = abstractGridPopulator;
    }

    public List<PathNode> InitGrid(Func<INodeable> getNode)
    {
        return this.abstractGridPopulator.PopulateGrid(getNode).Select(node => (PathNode)node).ToList();
    }
}