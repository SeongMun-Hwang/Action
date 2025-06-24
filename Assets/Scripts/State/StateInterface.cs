using UnityEngine;

public interface IState {
    void Enter();
    void Update();
    void Exit();
}
public class StateMachine
{
    public IState Current
    {
        get { return currentState; }
    }
    private IState currentState;
    public void ChangeState(IState newState)
    {
        if (currentState == newState) return;
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void Update()
    {
        currentState?.Update();
    }
}