using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idle : State
{
    private Unit unit;

    private int counter = 0;

    public  Action OnIdle = delegate { };
    public  Action EnteringIdle = delegate { };
    public  Action ExitingIdle = delegate { };

    public Action<Transform> TargetChange = delegate { };

    public Idle(Unit unit)
    {
        this.unit = unit;
    }

    public override void Execute()
    {
        OnIdle();
    }
    public override State Check( )
    {
        counter %= framesPerSphereCast;
        if (counter != 0)
        {
            counter++;
            return null;
        }
        counter++;


        switch (unit.inputState)
        {
            case Unit.InputState.DEFAULT:

                if (RadiousCheckTool.EnemyInsideAnArea(unit, unit.onSigthRange))
                    return unit.states[Unit.StateIdentifier.MOVING];
                return null;


            case Unit.InputState.ATTACK_MOVE:

                return unit.states[Unit.StateIdentifier.MOVING];


            case Unit.InputState.PLAYER_MOVE:

                return unit.states[Unit.StateIdentifier.MOVING];


            case Unit.InputState.STAY_IN_PLACE:

                Collider[] enemies;
                if (RadiousCheckTool.EnemyInsideAnArea(unit, unit.figthingRange, out enemies))
                {
                    Transform closestEnemy = unit.transform.position.ClosestColliderXZ(enemies).transform;
                    TargetChange(closestEnemy);
                    return unit.states[Unit.StateIdentifier.FIGHTING];
                }
                else
                    return null;

        }
        return null;

    }
    public override void OnExit()
    {
        ExitingIdle();
    }
    public override void OnEnter()
    {
        unit.SetTarget(null);
        EnteringIdle();
    }


}
