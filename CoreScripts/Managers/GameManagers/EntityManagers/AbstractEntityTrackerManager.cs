using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractEntityTrackerManager<T> : AbstractGameplayManager
{
    public Dictionary<T, LogicEntity> entityToLogicData;
    protected List<T> allEntities;
    private Dictionary<T, int> tByIndex;

    public AbstractEntityTrackerManager()
    {
        this.allEntities = new List<T>();
        this.entityToLogicData = new Dictionary<T, LogicEntity>();
        this.tByIndex = new Dictionary<T, int>();
    }

    public virtual LogicEntity GetLogicFromTracker(T tracker)
    {
        return this.entityToLogicData.DefaultGet(tracker, () => null);
    }

    public List<T> GetEntities(Func<T, bool> filterPredicate)
    {
        return this.allEntities.Where(filterPredicate).ToList();
    }

    public int GetEntitiesIndex(T entity)
    {
        return this.tByIndex.DefaultGet(entity, () =>
        {
            var entityIndex = this.allEntities.IndexOf(entity);
            this.tByIndex[entity] = entityIndex;
            return entityIndex;
        });
    }

    public T GetEntitiesByIndex(int entityIndex)
    {
        return this.allEntities[entityIndex];
    }

    public List<T> GetEntities()
    {
        return this.allEntities;
    }

    public void UnsubscribeEntity(T entity)
    {
        this.allEntities.Remove(entity);
        this.RemoveRelation(entity);
    }

    public void SubscribeEntity(T entity)
    {
        this.allEntities.Add(entity);
    }

    public void AddRelation(T entity, LogicEntity logicEntity)
    {
        this.entityToLogicData[entity] = logicEntity;
    }

    public void RemoveRelation(T entity)
    {
        if (this.entityToLogicData.ContainsKey(entity))
            this.entityToLogicData.Remove(entity);
    }

    public void EmptyTracker()
    {
        this.entityToLogicData.Clear();
        this.allEntities.Clear();
    }
}
