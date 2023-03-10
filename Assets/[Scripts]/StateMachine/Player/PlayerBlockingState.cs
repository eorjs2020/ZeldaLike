using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockInHash = Animator.StringToHash("ShieldBlock_In");
    private readonly int BlockingSpeedHash = Animator.StringToHash("Blend");
    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        stateMachine.Health.SetInvunerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockInHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if (!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
              
        
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvunerable(false);
    }   

    /*private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(BlockingSpeedHash, 0, 0.1f, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(BlockingSpeedHash, 1.0f, 0.1f, deltaTime);
        
    }*/
}
