using System;

public abstract class AbstractFSM<T, W> where W : AbstractFSMData
{
    public abstract event Action<IFSMState<W>, IFSMState<W>> OnStateChanged;

    public AbstractFSMStateDatabase<T, W> FSMStateDatabase { get; protected set; }
    public AbstractFSMTransitioner<T, W> FSMTransitioner { get; protected set; }
    public IFSMState<W> GetCurrentState { get; protected set; }
    public T GetCurrentType { get; protected set; }

    public AbstractFSM(AbstractFSMStateDatabase<T, W> fSMStateDatabase, AbstractFSMTransitioner<T, W> fSMTransitioner)
    {
        FSMStateDatabase = fSMStateDatabase;
        FSMTransitioner = fSMTransitioner;
    }

    public AbstractFSM()
    {
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

public abstract class AbstractFSMCustom<Database, Transitioner, State, Key, Data> where Data : AbstractFSMData where State : IFSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data> where Transitioner : AbstractFSMTransitionerCustomState<State, Key, Data>
{
    public abstract event Action<State, State> OnStateChanged;

    public Transitioner FSMTransitioner { get; protected set; }
    public Database FSMStateDatabase { get; protected set; }
    public State CurrentState { get; protected set; }
    public Key CurrentType { get; protected set; }

    public AbstractFSMCustom(Database fSMStateDatabase, Transitioner fSMTransitioner)
    {
        FSMStateDatabase = fSMStateDatabase;
        FSMTransitioner = fSMTransitioner;
    }

    public virtual void SetFSMTransitioner(Transitioner abstractFSMTransitioner)
    {
        this.FSMTransitioner = abstractFSMTransitioner;
    }

    public virtual void SetStateDatabase(Database abstractFSMStateDatabase)
    {
        this.FSMStateDatabase = abstractFSMStateDatabase;
    }

    public abstract void Feed(Key state, Data data = null);
}
