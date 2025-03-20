using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Animator animator;
    public InputActionAsset inputActions;

    private Vector2 movementInput;
    private Rigidbody rb;
    private InputAction moveAction;
    
    public Transform upperBody;
    public Transform weapon;

    public Vector3 spawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        spawnPosition = transform.position;

        moveAction = inputActions.FindAction("Player/Move");
        moveAction.Enable();
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    void Update()
    {
        Move();
        RotateUpperBody();
        UpdateAnimations();
    }

    void Move()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }
    
    void RotateUpperBody()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = hit.point - upperBody.position;
            direction.y = 0;
            upperBody.rotation = Quaternion.LookRotation(direction);

            if (weapon != null)
            {
                weapon.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    void UpdateAnimations()
    {
        float speed = movementInput.magnitude;
        animator.SetFloat("Speed", speed); 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void OnDestroy()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.Disable();
    }
}
