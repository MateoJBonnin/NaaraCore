using System;

public abstract class AbstractFSM<T, W> where W : AbstractFSMData
{
    public abstract event Action<FSMState<W>, FSMState<W>> OnStateChanged;

    public T GetCurrentType { get; protected set; }
    public FSMState<W> GetCurrentState { get; protected set; }
    //protected AbstractFSMTransitionsConfig<T, W> FSMTransitionsConfig { get; set; }
    protected AbstractFSMStateDatabase<T, W> FSMStateDatabase { get; set; }
    protected AbstractFSMTransitioner<T, W> FSMTransitioner { get; set; }

    public AbstractFSM(/*AbstractFSMTransitionsConfig<T, W> fSMTransitionsConfig, */AbstractFSMStateDatabase<T, W> fSMStateDatabase, AbstractFSMTransitioner<T, W> fSMTransitioner)
    {
        //FSMTransitionsConfig = fSMTransitionsConfig;
        FSMStateDatabase = fSMStateDatabase;
        FSMTransitioner = fSMTransitioner;
    }

    //public virtual void SetTransitionsConfig(AbstractFSMTransitionsConfig<T, W> abstractFSMTransitionsConfig)
    //{
    //    this.FSMTransitionsConfig = abstractFSMTransitionsConfig;
    //}

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