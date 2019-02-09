using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Unit unit;
    private Fighting fighting;

    [Header("Atack variables")]
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private int strenght;


    private bool onAttack = false;

    private void Start()
    {
        unit = GetComponent<Unit>();
        fighting = (Fighting)unit.states[Unit.StateIdentifier.FIGHTING];
        fighting.OnFigthing += Fight;
    }


    private void Fight()
    {
        if (! onAttack)
        {
            onAttack = true;
            fighting.attackEnded = false;
            StartCoroutine(StartAttack(unit.Target.GetComponent<Health>()));
        }
    }

    private IEnumerator StartAttack(Health targetHealth)
    {
        float secondsPerAttack = 1f / attackSpeed;
        //play animation
        Debug.Log("peleando...");
        yield return new WaitForSeconds(secondsPerAttack);
        targetHealth.ModifyHealth(-strenght);
        onAttack = false;
        fighting.attackEnded = true;
    }
}
