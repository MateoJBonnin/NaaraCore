using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIAction
{
    Action OnActionSucced { get; set; }
    Action OnActionInterrupted { get; set; }
    void ExecuteAction();
    void UpdateAction();
}