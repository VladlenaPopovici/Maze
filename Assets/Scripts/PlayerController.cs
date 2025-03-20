using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [HideInInspector] public Animator animator;
    public InputActionAsset inputActions;

    private Vector2 movementInput;
    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction finishAction;
    
    [HideInInspector] public Transform upperBody;
    public Transform weapon;

    [HideInInspector] public Vector3 spawnPosition;
    
    public float finishingDistance = 1f;
    public GameObject sword; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        spawnPosition = transform.position;

        finishAction = inputActions.FindAction("Player/Jump");
        
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
        if (finishAction.triggered)
        {
            TryToFinishEnemy();
        }
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
            upperBody.rotation = Quaternion.LookRotation(direction, Vector3.forward);

            if (weapon != null)
            {
                weapon.rotation = Quaternion.LookRotation(direction, Vector3.forward);
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

    void TryToFinishEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < finishingDistance)
            {
                animator.SetTrigger("Finishing");
                sword.SetActive(false);
                enemy.GetComponent<Enemy>().FinishEnemy();
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            UIManager.Instance.ShowFinishPrompt(true);
        }
    }

    void OnDestroy()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.Disable();
    }
}
