using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleGridPopulatorTechnic<T> : AbstractGridPopulatorTechnic<T> where T : Component, INodeable
{
    protected const int X_GRID_SIZE = 25;
    protected const int Z_GRID_SIZE = 25;

    protected Vector3 startingGridPosition;
    // Fix this mirroredGrid bool to an abstraction
    protected bool mirroredGrid;

    public SimpleGridPopulatorTechnic(Vector3 startingGridPosition, bool mirroredGrid = false)
    {
        this.startingGridPosition = startingGridPosition;
        this.mirroredGrid = mirroredGrid;
    }

    public override void ConfigureNodes(List<T> nodesToConfigure)
    {
        foreach (INodeable node in nodesToConfigure)
        {
            List<T> allNodes = new List<T>();
            allNodes.AddRange(node.GetNeighNodes().Select(n => (T)n).ToList());
            node.NeighNodes = allNodes.Select(n => (INodeable)n).ToList();
        }
    }

    public override List<INodeable> PopulateGrid(Func<INodeable> getNode)
    {
        List<INodeable> nodes = new List<INodeable>();
        int xStart = 0;
        int zStart = 0;

        Vector3 cellPosition = startingGridPosition;

        if (mirroredGrid)
        {
            xStart = -X_GRID_SIZE;
            zStart = -Z_GRID_SIZE;
        }

        for (int x = xStart; x < X_GRID_SIZE; x++)
            for (int z = zStart; z < Z_GRID_SIZE; z++)
            {
                T node = (T)getNode();
                node.gameObject.name = node.gameObject.name + " " + x.ToString() + "_" + z.ToString();
                node.transform.position = new Vector3(cellPosition.x + x, 0, cellPosition.z + z);
                nodes.Add(node);
            }

        return nodes;
    }
}
