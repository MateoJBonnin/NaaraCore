using System;
using System.Collections.Generic;
using System.Linq;

public class PathGridPopulator<T>
{
    public AbstractGridPopulatorTechnic<T> abstractGridPopulator;

    public PathGridPopulator(AbstractGridPopulatorTechnic<T> abstractGridPopulator)
    {
        this.abstractGridPopulator = abstractGridPopulator;
    }

    public List<T> InitGrid(Func<INodeable> getNode)
    {
        return this.abstractGridPopulator.PopulateGrid(getNode).Select(node => (T)node).ToList();
    }
}