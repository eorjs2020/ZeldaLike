using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector2 dodgingDirectionInput;

    private float remaningDodgeTime;
    private readonly int TargeingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargeingForwardSpeedHash = Animator.StringToHash("TargetingFoward");

    private const float CrossFadeDuration = 0.1f;

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;

        stateMachine.InputReader.DodgeEvent += OnDodge;

        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.Animator.CrossFadeInFixedTime(TargeingBlendTreeHash, CrossFadeDuration);
        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if(stateMachine.InputReader.IsBlocking && stateMachine.isShield)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);

        if (stateMachine.InputReader.IsDash)
        {
            Move(movement * stateMachine.FreeLookDashMovementSpeed, deltaTime);            
        }
        else
        {
            Move(movement * stateMachine.TragetingMovementSpeed, deltaTime);
        }
        UpdateAnimator(deltaTime);
        FaceTarget();

    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));

    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();
        
        if(remaningDodgeTime > 0f)
        {
            movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            remaningDodgeTime = Mathf.Max(remaningDodgeTime - deltaTime, 0f);

            
        }
        else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }        

        return movement;
    }

    private void OnDodge()
    {
        if(Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown) { return; }

        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remaningDodgeTime = stateMachine.DodgeDuration;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(TargeingForwardSpeedHash, 0, 0.1f, deltaTime);
            return;
        }
        if(stateMachine.InputReader.IsDash)
            stateMachine.Animator.SetFloat(TargeingForwardSpeedHash, 1.0f, 0.1f, deltaTime);
        else
            stateMachine.Animator.SetFloat(TargeingForwardSpeedHash, 0.5f, 0.1f, deltaTime);
    }
}
