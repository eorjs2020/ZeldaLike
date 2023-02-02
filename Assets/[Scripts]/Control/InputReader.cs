using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsDash { get; private set; }
    public bool IsBlocking { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;
    public event Action DashEvent;
    public event Action CookingState;
    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed || ChangeGameManager.Instance.inventroyOn) { return; }        
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed && ChangeGameManager.Instance.inventroyOn) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed || ChangeGameManager.Instance.inventroyOn) { return; }
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
       if(!context.performed || ChangeGameManager.Instance.inventroyOn) { return; }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed || ChangeGameManager.Instance.inventroyOn) { return; }

        CancelEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed || ChangeGameManager.Instance.inventroyOn)
        {            
            IsDash = true;
        }
        else if (context.canceled)
        { 
            IsDash = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
        if (context.performed || ChangeGameManager.Instance.inventroyOn)
        {
            IsAttacking = true;
        }
        else if(context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed || ChangeGameManager.Instance.inventroyOn)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed || ChangeGameManager.Instance.inventroyOn) { return; }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
