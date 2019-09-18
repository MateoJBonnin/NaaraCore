using System;

public class FSMStateLink<T>
{
    public T stateFrom;
    public T stateTo;

    public FSMStateLink(T stateFrom, T stateTo)
    {
        this.stateFrom = stateFrom;
        this.stateTo = stateTo;
    }
}