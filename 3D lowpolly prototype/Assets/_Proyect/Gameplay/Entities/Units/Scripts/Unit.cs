using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{


    #region prev
    //    public enum StateIdentifier {IDLE, MOVING, FIGHTING,};
    //    public enum InputState { DEFAULT, ATTACK_MOVE, PLAYER_MOVE, STAY_IN_PLACE };

    //    public bool friendly;
    //    [Header("Variables for state changes")]
    //    public Dictionary<StateIdentifier, State> states = new Dictionary<StateIdentifier, State>();

    //    public InputState inputState { get; private set; }
    //    private State actionState;


    //    public float figthingRange;
    //    public float onSigthRange;

    //    public Vector3 Destination { get; private set; } //used for the AMove/PMove
    //    public Transform Target { get; private set; }

    //    //Components
    //    private Health health;
    //    private Attack attack;
    //    private AutonomousMovement movement;
    //    private new Rigidbody rigidbody;




    //    private void Awake()
    //    {
    //        PopulateStateDictionary();
    //        actionState = states[StateIdentifier.IDLE];
    //        inputState = InputState.DEFAULT;
    //        attack = GetComponent<Attack>();
    //        health = GetComponent<Health>();
    //        movement = GetComponent<AutonomousMovement>();
    //        rigidbody = GetComponent<Rigidbody>();
    //        SubscribeToEvents();
    //    }
    //    private void Update()
    //    {
    //        RunAndChangeStates();
    //    }

    //    private void RunAndChangeStates()
    //    {
    //        State newState = actionState.Check();
    //        if (newState == null)
    //        {
    //            actionState.Execute();
    //        }
    //        else
    //        {
    //            Debug.Log($"from {actionState.ToString()} to {newState.ToString()}");
    //            actionState.OnExit();
    //            newState.OnEnter();

    //            actionState = newState;
    //        }
    //    }
    //    public void SetTarget(Transform newTarget)
    //    {
    //        Target = newTarget;
    //    }
    //    private void PopulateStateDictionary()
    //    {
    //        states.Add(StateIdentifier.IDLE, new Idle(this));
    //        states.Add(StateIdentifier.MOVING, new Moving(this));
    //        states.Add(StateIdentifier.FIGHTING, new Fighting(this));

    //    }

    //    private void ArribeAtDestination()
    //    {
    //        SetInputState(InputState.DEFAULT);
    //    }

    //    private void SetDestination(Vector3 destination)
    //    {
    //        Destination = destination;
    //        inputState = InputState.PLAYER_MOVE;
    //    }
    //    private void SetInputState(InputState newState)
    //    {
    //        inputState = newState;
    //    }

    //    private void SubscribeToEvents()
    //    {
    //        movement.OnDestinationChange += SetDestination;
    //        movement.OnDestinationArribe += ArribeAtDestination;

    //        Idle idle = (Idle)states[StateIdentifier.IDLE];
    //        idle.TargetChange += SetTarget;
    //        Moving moving = (Moving)states[StateIdentifier.MOVING];
    //        moving.TargetChange += SetTarget;
    //    }




    //    void OnDrawGizmos()
    //    {
    //        if (enableGizmos)
    //        {
    //            Gizmos.color = Color.blue;
    //            Gizmos.DrawWireSphere(transform.position, onSigthRange);

    //            Gizmos.color = Color.red;
    //            Gizmos.DrawWireSphere(transform.position, figthingRange);
    //        }

    //    }
    //    [Header("Gizmos")]
    //    public bool enableGizmos;

    #endregion
}
