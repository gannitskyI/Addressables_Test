using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkForce = 100f;
    [SerializeField] private float runForce = 200f;
    [SerializeField] private float jumpForce = 300f;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask enemyLayer;

    private IMoveable characterController;
    private IInputHandler inputHandler;
    private Animator animator;
    private IInteractable currentInteractable;
    private bool isMoving = false;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        InitializeSingleton();
        InitializeComponents();
        InitializeCharacterController();
        inputHandler = new InputHandler();
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void InitializeComponents()
    {
        if (!TryGetComponent(out Rigidbody rb))
        {
            Debug.LogError("Rigidbody component not found on PlayerController GameObject.");
            return;
        }

        if (!TryGetComponent(out animator))
        {
            Debug.LogError("Animator component not found on PlayerController GameObject.");
            return;
        }
    }

    private void InitializeCharacterController()
    {
        CharacterConfig config = new CharacterConfig(walkForce, runForce, jumpForce, enemyLayer);
        characterController = new CharacterController(GetComponent<Rigidbody>(), animator, transform, config);
    }

    private void Update()
    {
        Vector3 direction = inputHandler.GetInputDirection();
        bool isRunning = inputHandler.IsRunning();
        bool isJumping = inputHandler.IsJumping();
 
        isMoving = direction != Vector3.zero || isRunning;

        if (direction != Vector3.zero)
        {
            characterController.Move(direction, isRunning);
        }
        else
        {
            characterController.Idle();
        }

        if (isJumping)
        {
            characterController.Jump();
        }
 
        if (!isMoving && inputHandler.IsAttacking())
        {
            characterController.Attack();
        }

        if (inputHandler.IsInteracting() && currentInteractable != null)
        {
            currentInteractable.Interact();
        }

    (characterController as CharacterController)?.Update();
    }

    public void EnableInteraction(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    public void DisableInteraction(IInteractable interactable)
    {
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }
}
