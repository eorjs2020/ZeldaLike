using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("Blend Tree");

    private readonly int FreeLookSpeedHash = Animator.StringToHash("Blend");

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }    

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DashEvent += OnDash;
       
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }
    
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking && stateMachine.isSword)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();
        if (stateMachine.InputReader.IsDash)
        {
            Move(movement * stateMachine.FreeLookDashMovementSpeed, deltaTime);
        }
        else
        {
            Move(movement * stateMachine.TragetingMovementSpeed, deltaTime);
        }   

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        if (stateMachine.InputReader.IsDash)
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, AnimatorDampTime, deltaTime);
        else
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.5f, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnTarget()
    {
        
        if (!stateMachine.Targeter.SelectTarget()) { Debug.Log("Cant"); return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private void OnDash()
    {
        
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + 
            right * stateMachine.InputReader.MovementValue.x;
    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        if (ChangeGameManager.Instance.inventroyOn) { return; }
        stateMachine.transform.rotation = Quaternion.Lerp( 
           stateMachine.transform.rotation, 
           Quaternion.LookRotation(movement),
           deltaTime * stateMachine.RotationDamping);
    }
}
