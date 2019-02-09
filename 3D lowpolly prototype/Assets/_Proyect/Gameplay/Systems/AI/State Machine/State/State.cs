using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected const int framesPerSphereCast = 32;

    public abstract void Execute();
    public abstract State Check();
    public abstract void OnEnter();
    public abstract void OnExit();
}
