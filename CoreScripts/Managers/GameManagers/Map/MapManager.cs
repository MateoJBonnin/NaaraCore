using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Factory;
using System;
using System.Linq;

namespace Managers
{
    public class MapManager<T> where T : Component, INodeable, IPooleable
    {
        private const string GRID_PARENT_NAME = "PathNodesGridMap";
        private const string GRID_PARENT_NAME_UNUSED = "PathNodesGridMapUnused";
        private const int DEFAULT_POOL_INIT_AMOUNT = 4;

        public Action OnMapChanged;
        public Action OnMapReady;

        protected PooleableFactory<T> PathNodePoolFactory { get; set; }
        protected List<T> PathNodes { get; set; }

        private Transform pathNodesParentUnused;
        private Transform pathNodesParent;

        public void Init(Transform gridTransform, T mapNodePrefab, PathGridPopulator<T> PathGridPopulator)
        {
            GameObject gridParent = new GameObject(GRID_PARENT_NAME);
            gridParent.transform.SetParent(gridTransform, true);
            this.pathNodesParent = gridParent.transform;

            GameObject gridParentUnused = new GameObject(GRID_PARENT_NAME_UNUSED);
            gridParentUnused.transform.SetParent(gridTransform, true);
            this.pathNodesParentUnused = gridParentUnused.transform;

            this.PathNodePoolFactory = new PooleableFactory<T>(() => GameObject.Instantiate(mapNodePrefab, this.pathNodesParentUnused), DEFAULT_POOL_INIT_AMOUNT, () => this.OnMapReady?.Invoke());
            this.PathNodes = PathGridPopulator.InitGrid(this.GetNode);
            PathGridPopulator.abstractGridPopulator.ConfigureNodes(this.PathNodes);
        }

        public void CreatePathNodeAt(Vector3 position)
        {

        }

        public T GetClosestNodeTo(Vector3 position)
        {
            return this.PathNodes.OrderBy(node => Vector3.SqrMagnitude(node.transform.position - position)).First();
        }

        public T GetClosestNodeTo(AbstractViewEntity viewEntity)
        {
            return this.GetClosestNodeTo(viewEntity.transform.position);
        }

        public T GetClosestNodeTo(LogicEntity logicEntity)
        {
            return this.GetClosestNodeTo(logicEntity.ViewEntity);
        }

        public List<T> GetRandomNodes(AbstractMapNodesRequester<T> mapNodesRequester)
        {
            return mapNodesRequester.GetNodes(this.PathNodes);
        }

        public List<T> GetPathNodes()
        {
            return PathNodes;
        }

        protected virtual T GetNode()
        {
            T node = this.PathNodePoolFactory.Create();
            node.OnReturnedItem += this.ReturnNode;
            node.transform.SetParent(this.pathNodesParent, true);
            node.EnableObject();
            return node;
        }

        private void ReturnNode(IPooleable node)
        {
            Transform nodeTransform = (Transform)node;
            nodeTransform.SetParent(this.pathNodesParentUnused, true);
            node.OnReturnedItem -= this.ReturnNode;
            node.DisableObject();
            this.PathNodePoolFactory.ReturnPoolItem((T)node);
        }
    }
}