using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{

    //[SerializeField] Transform playerInputSpace = default;

    //[SerializeField] private float speed = 10.0f;
    //[SerializeField] private float jumpHeight = 20.0f;
 
    private Rigidbody playerRb;
    private Collider playerCollider;
    private bool isGrounded = true;


    private PlayerInput playerInput;
    private InputSystem_Actions inputActions;

    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    //private AnimatorStateInfo layer2CurrentState;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");

    [Header("Debug QOA Stuff")]
    private bool cursorVisible = true;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump;
        //inputActions.Player.ShowCursor.performed += ShowCursor;

    }

    private void Start()
    {
        ToggleVisibleCursor();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MovePlayer()
    {

        Vector2 inputMovementVector = inputActions.Player.Move.ReadValue<Vector2>();
        
        float horizontal = inputMovementVector.x;
        float vertical = inputMovementVector.y;
        anim.SetFloat("Speed",vertical);
        anim.SetFloat("Direction", horizontal);
        
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Need To Jump");
        }
    }

    private void ShowCursor(InputAction.CallbackContext context)
    {
        ToggleVisibleCursor();
    }

    private void ToggleVisibleCursor()
    {
        if (cursorVisible)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            cursorVisible = false;
            Debug.Log("Cursor off");
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            cursorVisible = true;
            Debug.Log("Cursor on");
        }
    }

}
