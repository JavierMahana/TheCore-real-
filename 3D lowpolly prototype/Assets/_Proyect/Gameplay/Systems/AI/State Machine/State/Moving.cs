using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Moving : State
{
    private Unit unit;

    private int counter;
    public bool EnemyOnSigthOnAttackMove { get; private set; } //used to change bewtween target and destination on attack move

    public Action<Vector3> OnMoving = delegate { };
    public Action EnteringMoving = delegate { };
    public Action ExitingMoving = delegate { };
    public Action<Transform> TargetChange = delegate { };

    public Moving(Unit unit)
    {
        this.unit = unit;
        timePassed = 0;
    }
    float timePassed;

    public override void Execute()
    {
        switch (unit.inputState)
        {
            case Unit.InputState.ATTACK_MOVE:

                if (EnemyOnSigthOnAttackMove)
                    OnMoving(unit.Target.position);
                else
                    OnMoving(unit.Destination);
                break;


            case Unit.InputState.PLAYER_MOVE:

                OnMoving(unit.Destination);
                break;


            default:

                if (unit.Target == null)
                {
                    UpdateUnitTarget();
                }
                OnMoving(unit.Target.position);
                break;
        }
        
    }
    public override State Check()
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

                if (RadiousCheckTool.EnemyInsideAnArea(unit, unit.onSigthRange, out Collider[] enemies) == false)
                    return unit.states[Unit.StateIdentifier.IDLE];

                Transform closestEnemy = unit.transform.position.ClosestColliderXZ(enemies).transform;
                UpdateUnitTarget(closestEnemy);
                if (unit.transform.position.DistanceXZ(closestEnemy.position) <= unit.figthingRange)
                    return unit.states[Unit.StateIdentifier.FIGHTING];
                else
                    return null;


            case Unit.InputState.ATTACK_MOVE:

                if (RadiousCheckTool.EnemyInsideAnArea(unit, unit.onSigthRange, out Collider[] entities))
                {
                    Transform closestEntity = unit.transform.position.ClosestColliderXZ(entities).transform;
                    EnemyOnSigthOnAttackMove = true;
                    UpdateUnitTarget(closestEntity);
                    if (unit.transform.position.DistanceXZ(closestEntity.position) <= unit.figthingRange)
                    {
                        return unit.states[Unit.StateIdentifier.FIGHTING];
                    }
                }
                else
                    EnemyOnSigthOnAttackMove = false;
                    return null;


            case Unit.InputState.PLAYER_MOVE:
                return null; // ------------------------- si llega a su destino

            case Unit.InputState.STAY_IN_PLACE:

                return unit.states[Unit.StateIdentifier.IDLE];


        }
        return null;
    }
    public override void OnExit()
    {
        ExitingMoving();
    }
    public override void OnEnter()
    {
        EnteringMoving();
    }

    private void UpdateUnitTarget()
    {
        if (RadiousCheckTool.EnemyInsideAnArea(unit, unit.onSigthRange, out Collider[] enemies) == false)
            return;

        Transform closestEnemy = unit.transform.position.ClosestColliderXZ(enemies).transform;
        TargetChange(closestEnemy);
    }
    private void UpdateUnitTarget(Transform newTarget)
    {
        TargetChange(newTarget);
    }
}
