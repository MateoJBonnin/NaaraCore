using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIWorldState
{
    public int stepsCount = 0;
    public IAIGOAPAction builderAction;
    public Dictionary<Type, AIWorldStateSymbol> WorldState { get; private set; }

    public AIWorldState()
    {
        this.WorldState = new Dictionary<Type, AIWorldStateSymbol>();
        this.RegisterSymbol(new WSSCActionCount());
    }

    public AIWorldState(Dictionary<Type, AIWorldStateSymbol> worldState)
    {
        this.WorldState = worldState;
        this.RegisterSymbol(new WSSCActionCount());
    }

    public T GetStateSymbol<T>() where T : AIWorldStateSymbol
    {
        AIWorldStateSymbol aIWorldStateSymbol = null;
        WorldState.TryGetValue(typeof(T), out aIWorldStateSymbol);
        return aIWorldStateSymbol as T;
    }

    public void RegisterSymbol(AIWorldStateSymbol stateSymbol)
    {
        this.WorldState[stateSymbol.GetType()] = stateSymbol;
    }

    public void RemoveSymbol(AIWorldStateSymbol stateSymbol)
    {
        this.WorldState.Remove(stateSymbol.GetType());
    }

    public bool IsSatisfiedWith(AIWorldState aIWorldState)
    {
        return this.WorldState.All(state => state.Value.IsSatisfied(aIWorldState));
    }

    public void UpdateState(AIWorldState worldState)
    {
        this.WorldState.Update(worldState.WorldState);
    }

    public List<AIWorldStateSymbol> GetNotSatisfiedWorldSymbols(AIWorldState goalWorld)
    {
        List<AIWorldStateSymbol> aIWorldStateSymbols = this.WorldState.Values.ToList();
        return aIWorldStateSymbols.Where(state => !state.IsSatisfied(goalWorld)).ToList();
    }

    public static AIWorldState CopyWorldState(AIWorldState aIWorldState)
    {
        return new AIWorldState(new Dictionary<Type, AIWorldStateSymbol>(aIWorldState.WorldState));
    }

    public static List<AIWorldStateSymbol> GetWorldSymbols(AIWorldState aIWorldState)
    {
        List<AIWorldStateSymbol> aIWorldStateSymbols = aIWorldState.WorldState.Values.ToList();
        return aIWorldStateSymbols;
    }

    //pensar si hacer un jsonobject con toda la informacion (mepa que no por ej en caso de necesitar informacion tambien de obstaculos por ej)
    //hacer varios aiworlstatesymbols espeicificos, con solamente la data necesaria, como el target, el entity del goap, los inventarios, los objetos de interes
    //quiza la ultima es la mejor, podemos ir agregando por ej objetos de interes a medida que los sensores los detectan
    //una lista dre aiworldstatesymbols (por ahi es muy pesado porque tendriamos que estar buscando en la lista todo el tiempo)
    //literalmente referencias a todas las cosas que sean de interes.

    //en caso de los requirements, solo tenemos que decir true or false, 
    //por ej, enemy.hp >= 50
    //podemos tener directamente una copia de la referencia de todos los objetos,
    //seguir pensando la heuristica

    // una posible herusitica seria contar la cantidad de acciones hasta el momento
    // tambien en caso de usar la cantidad de actions no seria lo ideal, porque por ej caminar y teletransportase equivalen a una accion pero teletransportarse es instanteo, entonces la velocidad tambien jugaria un rol.
}