using UnityEngine;
 
public class CharacterConfig
{
    public float WalkForce { get; }
    public float RunForce { get; }
    public float JumpForce { get; }
    public float AttackDamage { get; } = 25f;
    public float AttackRange { get; } = 2f;
    public LayerMask EnemyLayer { get; }

    public CharacterConfig(float walkForce, float runForce, float jumpForce, LayerMask enemyLayer)
    {
        WalkForce = walkForce;
        RunForce = runForce;
        JumpForce = jumpForce;
        EnemyLayer = enemyLayer;
    }
}