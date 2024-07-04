using UnityEngine;

public interface IInputHandler
{
    Vector3 GetInputDirection();
    bool IsRunning();
    bool IsJumping();
    bool IsAttacking();
    bool IsInteracting();
}