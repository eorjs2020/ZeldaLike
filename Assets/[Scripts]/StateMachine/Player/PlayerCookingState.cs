using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCookingState : PlayerBaseState
{

    private readonly int Cooking = Animator.StringToHash("Cooking");

    private const float CrossFadeDuration = 0.1f;

    public PlayerCookingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(Cooking, CrossFadeDuration);        
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {        
        
    }

    
    
}
