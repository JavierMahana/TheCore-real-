using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutonomousMovement : MonoBehaviour
{
    
    public Action<Vector3> OnDestinationChange = delegate { };
    public Action OnDestinationArribe = delegate { };

    private Unit unit;
    private Moving moving;

    new private Rigidbody rigidbody;

    [SerializeField]
    private float decreaseVelRadious;
    [SerializeField]
    private float stopRadious;
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float movementSpeed;

    private void Start()
    {
        unit = GetComponent<Unit>();
        moving = (Moving)unit.states[Unit.StateIdentifier.MOVING];
        rigidbody = GetComponent<Rigidbody>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        moving.OnMoving += MoveTo;
    }

    public void ChangeDestination(Vector3 newDestiantion)
    {
        OnDestinationChange(newDestiantion);
    }

    public void MoveTo(Transform target)
    {
        Vector2 steering2D = SteeringBehabiours.ArribeXZ(rigidbody , new Vector3(target.position.x, target.position.z), movementSpeed, maxForce, decreaseVelRadious, stopRadious);
        Vector3 forceToApply = new Vector3(steering2D.x, 0, steering2D.y);
        rigidbody.AddForce(forceToApply, ForceMode.Impulse);
    }
    public void MoveTo(Vector3 destination)
    {
        Vector2 steering2D = SteeringBehabiours.ArribeXZ(rigidbody, new Vector3(destination.x, destination.z), movementSpeed, maxForce, decreaseVelRadious, stopRadious);
        Vector3 forceToApply = new Vector3(steering2D.x, 0, steering2D.y);
        rigidbody.AddForce(forceToApply, ForceMode.Impulse);

        if (unit.inputState == Unit.InputState.PLAYER_MOVE || unit.inputState == Unit.InputState.ATTACK_MOVE && moving.EnemyOnSigthOnAttackMove)
        {
            if (unit.transform.position.DistanceXZ(unit.Destination) <= stopRadious)
            {
                OnDestinationArribe();
            }
        }
       
    }

    public bool showGizmos;
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, decreaseVelRadious);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, stopRadious);
        }
        
    }
}
