using UnityEngine;

public class InputHandler : IInputHandler
{
    public Vector3 GetInputDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        return direction;
    }

    public bool IsRunning()
    {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
               && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
    }

    public bool IsJumping()
    {
        return Input.GetKeyDown(KeyCode.Space); 
    }
    public bool IsAttacking()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
    public bool IsReloadingScene()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool IsInteracting()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
