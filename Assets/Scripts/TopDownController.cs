using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector2 lastMoveDirection = Vector2.down;
    public Joystick joystick;

    [Header("Key")]
    public bool hasKey = false;
    public Key carryKey;

    [Header("Audio")]
    public AudioClip shiftSound;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private AudioSource audioSource;

    private PlayerControls controls;

    private float toggleCooldown = 1f;
    private float lastToggleTime = -Mathf.Infinity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        controls.Player.Interact.performed += OnToggleWorld;
    }

    void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;

        controls.Player.Interact.performed -= OnToggleWorld;

        controls.Disable();
    }

    void Update()
    {
        // Mobile joystick input
        if (joystick != null)
        {
            Vector2 joystickInput = new Vector2(
                joystick.Horizontal,
                joystick.Vertical
            );

            ProcessInput(joystickInput);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 keyboardInput = context.ReadValue<Vector2>();

        ProcessInput(keyboardInput);
    }

    private void ProcessInput(Vector2 input)
    {
        // Prevent tiny joystick drift
        if (input.magnitude < 0.1f)
        {
            movement = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        // Restrict to 4 directions
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            movement = input.x > 0
                ? new Vector2(1, 0)
                : new Vector2(-1, 0);

            animator.SetInteger("direction", input.x > 0 ? 3 : 2);
        }
        else
        {
            movement = input.y > 0
                ? new Vector2(0, 1)
                : new Vector2(0, -1);

            animator.SetInteger("direction", input.y > 0 ? 0 : 1);
        }

        lastMoveDirection = movement;

        animator.SetBool("isMoving", true);
    }

    private void OnToggleWorld(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        MobileShiftWorld();
    }

    public void MobileShiftWorld()
    {
        if (Time.time < lastToggleTime + toggleCooldown)
            return;

        lastToggleTime = Time.time;

        StartCoroutine(ShiftWorld());
    }

    private IEnumerator ShiftWorld()
    {
        animator.SetTrigger("onShift");

        if (shiftSound != null)
        {
            audioSource.PlayOneShot(shiftSound);
        }

        yield return new WaitForSeconds(0.5f);

        WorldStateManager.Instance.ToggleWorld();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}