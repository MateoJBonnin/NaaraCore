using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleGridPopulatorTechnic : AbstractGridPopulatorTechnic
{
    protected const int X_GRID_SIZE = 25;
    protected const int Z_GRID_SIZE = 25;

    protected Vector3 startingGridPosition;
    protected bool mirroredGrid;

    private List<INodeable> nodes;

    public SimpleGridPopulatorTechnic(Vector3 startingGridPosition, bool mirroredGrid = false)
    {
        this.nodes = new List<INodeable>();
        this.startingGridPosition = startingGridPosition;
        this.mirroredGrid = mirroredGrid;
    }

    public override void ConfigureNodes()
    {
        foreach (INodeable node in nodes)
        {
            List<PathNode> allNodes = MapManager.Instance.GetPathNodes();
            allNodes = allNodes
                .Where(n => Vector3.SqrMagnitude(n.transform.position - ((PathNode)node).transform.position) <= n.connectionsRadius)
                .Where(n => n != (PathNode)node)
                .ToList();

            node.NeighNodes = allNodes.Select(n => (INodeable)n).ToList();
        }
    }

    public override List<INodeable> PopulateGrid(Func<INodeable> getNode)
    {
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
                PathNode node = (PathNode)getNode();
                node.gameObject.name = node.gameObject.name + " " + x.ToString() + "_" + z.ToString();
                node.transform.position = new Vector3(cellPosition.x + x, 0, cellPosition.z + z);
                nodes.Add(node);
            }

        return nodes;
    }
}
