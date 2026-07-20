using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Kinematic 2D player movement. Uses a Kinematic-mode Rigidbody2D
/// for collision detection only — gravity and movement are applied
/// manually via MovePosition(), not by Unity's physics solver.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityStrength = 20f;
    [SerializeField] private float groundCheckThickness = 0.08f; // height of the overlap-check box — see Rationale below
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpSpeed = 8f; // Upward velocity applied on jump press    

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool isGrounded;

    // Caches the Rigidbody2D and CapsuleCollider2D references once at
    // startup rather than calling GetComponent every frame. The collider
    // reference matters specifically for CheckGrounded() — see its comment.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Runs on Unity's physics tick. Combines horizontal input and the
    // current vertical velocity into one movement vector, then applies it
    // via MovePosition() — the Kinematic-mode equivalent of "move the
    // player," since this Rigidbody2D doesn't respond to physics forces.
    private void FixedUpdate()
    {
        CheckGrounded();
        ApplyGravity();

        Vector2 horizontalMove = new Vector2(moveInput.x * moveSpeed, 0f);
        Vector2 verticalMove = new Vector2(0f, verticalVelocity);
        Vector2 totalMove = (horizontalMove + verticalMove) * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + totalMove);
    }

    // Checks for ground contact using a thin overlap box centered on the
    // collider's bottom edge, rather than a raycast that travels some
    // distance downward looking for ground. This is the deliberate fix for
    // a structural problem with the raycast approach: a raycast only
    // reports "grounded" once its tip — cast from the collider's bottom —
    // has traveled far enough to touch Ground, which means detection fires
    // slightly *before* the collider has actually reached the surface,
    // leaving a small but clearly visible gap once the fall stops. An
    // overlap box has no travel distance to create that gap — it simply
    // asks "is anything occupying this thin slice of space right now,"
    // checked at the collider's actual position each frame. The box is
    // centered exactly on col.bounds.min.y (not above or below it) and is
    // only groundCheckThickness tall, so it reads as touching the surface
    // the instant the collider's bottom edge reaches it — no earlier, no
    // later. Width is taken directly from the collider's own bounds so
    // this works correctly regardless of how the Capsule Collider is
    // sized. Also zeroes out downward velocity on landing, so gravity
    // doesn't keep accumulating into the ground.
    private void CheckGrounded()
    {
        Vector2 boxCenter = new Vector2(transform.position.x, col.bounds.min.y);
        Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, groundCheckThickness);

        // Half this box sits inside the Player's own collider and half
        // sits below it — but the groundLayer filter means only Ground
        // (or anything else on that layer) is ever detected, never the
        // Player's own collider. This depends directly on the Player
        // GameObject being on a different Layer than groundLayer (Step 25)
        // — without that separation, this box would self-detect exactly
        // like the original floating bug, just via a different mechanism.
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);
        isGrounded = hit != null;

        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = 0f;
    }

    // Manually accumulates downward velocity over time, standing in for
    // what Unity's physics solver would normally do — only while airborne,
    // since CheckGrounded() already zeroes velocity on landing.
    private void ApplyGravity()
    {
        if (!isGrounded)
            verticalVelocity -= gravityStrength * Time.fixedDeltaTime;
    }

    // Wired explicitly in the Inspector via PlayerInput's Invoke Unity Events
    // behavior — the Move action's event slot is dragged to this GameObject
    // and this method chosen from the dropdown. The name is arbitrary; it is
    // never pattern-matched by the Input System. The parameter type must be
    // InputAction.CallbackContext, not InputValue — Invoke Unity Events
    // exposes each action as a UnityEvent<InputAction.CallbackContext>, and
    // only methods matching that exact parameter type appear in the
    // Inspector's function dropdown.
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Wired to the Jump action's Invoke Unity Events slot in the Inspector — same
    // pattern as HandleMoveInput. Jump is a Button action, so context.performed
    // fires once on press, not continuously while held.
    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }
    }
}