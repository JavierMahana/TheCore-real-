using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fighting : State
{

    private Unit unit;
    public bool attackEnded = true;

    public Action OnFigthing = delegate {};
    public Action EnteringFigthing = delegate { };
    public Action ExitingFigthing = delegate { };


    public Fighting(Unit unit)
    {
        this.unit = unit;
    }

    public override void Execute()
    {
        OnFigthing();
    }
    public override State Check()
    {
        switch (unit.inputState)
        {
            case Unit.InputState.PLAYER_MOVE:

                return unit.states[Unit.StateIdentifier.MOVING];


            case Unit.InputState.STAY_IN_PLACE:

                if (!unit.Target.gameObject.activeInHierarchy)
                    return unit.states[Unit.StateIdentifier.IDLE];
                if (attackEnded)
                {
                    if (unit.transform.position.DistanceXZ(unit.Target.position) > unit.figthingRange)
                        return unit.states[Unit.StateIdentifier.IDLE];
                }
                return null;

            default:
                if (!unit.Target.gameObject.activeInHierarchy)
                    return unit.states[Unit.StateIdentifier.MOVING];
                if (attackEnded)
                {
                    if (unit.transform.position.DistanceXZ(unit.Target.position) > unit.figthingRange)
                        return unit.states[Unit.StateIdentifier.MOVING];
                }
                return null;

        }
    }
    public override void OnExit()
    {
        ExitingFigthing();
    }
    public override void OnEnter()
    {
        EnteringFigthing();
    }

}
