using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractExternalStateDeserializer : AbstractStateDeserializer
{
    protected string externalPath;

    protected AbstractExternalStateDeserializer(string externalPath)
    {
        this.externalPath = externalPath;
    }
}
