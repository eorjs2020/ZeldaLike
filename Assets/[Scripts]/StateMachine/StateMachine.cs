using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    private ChangeGameManager gameManager;
    // Start is called before the first frame update
    public void SwitchState(State newState)
    {
        gameManager = GameObject.FindObjectOfType<ChangeGameManager>();
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager.is3D)
            currentState?.Tick(Time.deltaTime);
    }
}
