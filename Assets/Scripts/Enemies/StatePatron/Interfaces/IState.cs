using UnityEngine;

public interface IState
{
    // Methods for each state
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
}
