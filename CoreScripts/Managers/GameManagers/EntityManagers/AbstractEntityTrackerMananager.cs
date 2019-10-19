using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractEntityTrackerManager<T> : AutoInitManager
{
    public Dictionary<T, LogicEntity> entityToLogicData;
    protected List<T> allEntities;

    public AbstractEntityTrackerManager()
    {
        this.allEntities = new List<T>();
        this.entityToLogicData = new Dictionary<T, LogicEntity>();
    }

    public virtual LogicEntity GetLogicFromTracker(T tracker)
    {
        return this.entityToLogicData.DefaultGet(tracker, () => null);
    }

    public List<T> GetEntities(Func<T, bool> filterPredicate)
    {
        return this.allEntities.Where(filterPredicate).ToList();
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
        this.allEntities.Clear();
    }
}
