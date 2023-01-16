using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    private readonly int JumpHash = Animator.StringToHash("Jump_In");

    private Vector3 momentum;

    private const float CrossFadeDuration = 0.1f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        SoundManager.Instance.PlaySoundFX(Sound.JUMP, Chanel.PLAYER_SOUND_FX);
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);        
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);

        if(stateMachine.Controller.velocity.y  <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        FaceTarget();
    }

    public override void Exit()
    {
       
    }
   
}
