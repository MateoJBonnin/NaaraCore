using Managers;

public class BlockedReadyManagerContainer : AbstractManagerContainer<Manager>
{
    public BlockedReadyManagerContainer(Manager manager) : base(manager)
    {
    }

    public override void SetState(ManagerReadyStates state)
    {
        if (state != ManagerReadyStates.Ready)
        {
            base.SetState(state);
        }
    }
}