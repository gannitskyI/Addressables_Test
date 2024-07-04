using UnityEngine;

public class CharacterController : IMoveable
{
    private readonly CharacterConfig _config;
    private float _moveSpeed;
    private readonly Rigidbody _rigidbody;
    private readonly Animator _animator;
    private readonly Transform _transform;
    private bool _isJumping;
    private bool _wasGrounded;

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public CharacterController(Rigidbody rigidbody, Animator animator, Transform transform, CharacterConfig config)
    {
        _rigidbody = rigidbody;
        _animator = animator;
        _transform = transform;
        _config = config;
        _moveSpeed = config.WalkForce;
 
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Move(Vector3 direction, bool isRunning)
    {
        float force = isRunning ? _config.RunForce : _config.WalkForce;
        _rigidbody.AddForce(direction * force);
         
        _moveSpeed = force;
         
        SetAnimatorState("isRunning", isRunning);
        SetAnimatorState("isWalking", !isRunning);
         
        UpdateRotation(direction);
 
        FixZPosition();
    }

    private void UpdateRotation(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            _transform.rotation = direction.x > 0 ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);
        }
    }

    public void Idle()
    {
        SetAnimatorState("isWalking", false);
        SetAnimatorState("isRunning", false);
 
        FixZPosition();
    }

    public void Jump()
    {
        if (IsGrounded() && !_isJumping)
        {
            _rigidbody.AddForce(Vector3.up * _config.JumpForce, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
            _isJumping = true;
            _wasGrounded = false;
        }
 
        FixZPosition();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(_transform.position, Vector3.down, 0.1f);
    }

    public void Update()
    {
        bool isGrounded = IsGrounded();
          
        if (isGrounded && _isJumping && !_wasGrounded)
        {
            _animator.SetTrigger("Land");
            _isJumping = false;
        }

        _wasGrounded = isGrounded;
         
        FixZPosition();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        DoAttack();
    }

    private void DoAttack()
    {
      
        Collider[] hitEnemies = Physics.OverlapSphere(_transform.position, _config.AttackRange, _config.EnemyLayer);

        foreach (Collider enemyCollider in hitEnemies)
        {
            if (enemyCollider.TryGetComponent(out IAttackable attackable))
            {
                attackable.TakeDamage(_config.AttackDamage);

                // Update enemy health bar if available
                if (enemyCollider.TryGetComponent(out Enemy enemy) && enemy.healthBar != null)
                {
                    enemy.healthBar.fillAmount = enemy.currentHealth / enemy.maxHealth;
                }
            }
        }
    }

    private void FixZPosition()
    {
        Vector3 position = _transform.position;
        position.z = 16.5f;  
        _transform.position = position;
    }

    private void SetAnimatorState(string state, bool value)
    {
        _animator.SetBool(state, value);
    }
}
