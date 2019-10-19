using System;

public abstract class AbstractFSM<T, W> where W : AbstractFSMData
{
    public abstract event Action<FSMState<W>, FSMState<W>> OnStateChanged;

    public AbstractFSMStateDatabase<T, W> FSMStateDatabase { get; protected set; }
    public AbstractFSMTransitioner<T, W> FSMTransitioner { get; protected set; }
    public FSMState<W> GetCurrentState { get; protected set; }
    public T GetCurrentType { get; protected set; }

    public AbstractFSM(AbstractFSMStateDatabase<T, W> fSMStateDatabase, AbstractFSMTransitioner<T, W> fSMTransitioner)
    {
        FSMStateDatabase = fSMStateDatabase;
        FSMTransitioner = fSMTransitioner;
    }

    public virtual void SetFSMTransitioner(AbstractFSMTransitioner<T, W> abstractFSMTransitioner)
    {
        this.FSMTransitioner = abstractFSMTransitioner;
    }

    public virtual void SetStateDatabase(AbstractFSMStateDatabase<T, W> abstractFSMStateDatabase)
    {
        this.FSMStateDatabase = abstractFSMStateDatabase;
    }

    public abstract void Feed(T state, W data = null);
}