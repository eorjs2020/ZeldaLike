using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        LeftEnemy.Instance.KillEnemy();
        if (stateMachine.Ragdoll == null)
        {
            stateMachine.Animator.Play("Death");
        }
        else
        {
            stateMachine.Ragdoll?.ToggleRagdoll(true);
        }
        
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
    }

    public override void Tick(float deltaTime) 
    { 

    }
    public override void Exit() { }
}
