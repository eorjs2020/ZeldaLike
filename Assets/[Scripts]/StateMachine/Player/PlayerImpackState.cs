using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpackState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");

    private const float CrossFadeDuration = 0.1f;

    private float duration = 1f;

    public PlayerImpackState(PlayerStateMachine stateMachine) : base(stateMachine) { }   

    public override void Enter()
    {

        SoundManager.Instance.PlaySoundFX(Sound.HURT, Chanel.PLAYER_HURT_FX);
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        
    }    
}
