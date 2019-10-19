using System;

public interface IAIAction
{
    Action OnActionSucced { get; set; }
    Action OnActionInterrupted { get; set; }
    void ExecuteAction();
    void UpdateAction();
}