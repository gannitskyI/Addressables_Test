using UnityEngine;
public interface IMoveable
{
    public float MoveSpeed { get; set; }

    void Move(Vector3 direction, bool isRunning);
    void Idle();
    void Jump();
    void Attack();
}
