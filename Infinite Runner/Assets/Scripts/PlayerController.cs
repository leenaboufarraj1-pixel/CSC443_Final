using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes")]
    [SerializeField] private float laneOffset = 2f;
    [SerializeField, Min(1)] private int laneCount = 3;
    [SerializeField] private float laneSwitchSpeed = 14f;

    [Header("Jump")]
    [SerializeField] private float jumpVelocity = 8f;
    [SerializeField] private float doubleJumpVelocity = 6f;
    [SerializeField] private float gravity = -25f;

    private int _jumpCount = 0;
    private const int MaxJumps = 2;

    private int _laneIndex;
    private float _y;
    private float _yVel;
    private Vector2 _prevMove;

    [Header("Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 0.5f;

    private float _currentGroundY = 0f;

    void Awake()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        // 1. Apply Gravity and Vertical Movement
        _yVel += gravity * Time.deltaTime;
        _y += _yVel * Time.deltaTime;

        // 2. RAYCASTING: Detect the floor or train roofs
        // Start the ray at waist height (0.7f) so it hits the roof while jumping/landing
        Vector3 rayStart = transform.position + Vector3.up * 0.7f;

        // We look downward further than the current height to 'find' the upcoming surface
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 2.0f, groundLayer))
        {
            _currentGroundY = hit.point.y;
        }
        else
        {
            _currentGroundY = 0f;
        }

        // 3. Grounding Logic: Snap to surface if falling onto it
        if (_y <= _currentGroundY && _yVel <= 0)
        {
            _y = _currentGroundY;
            _yVel = 0f;
            _jumpCount = 0;
        }

        // 4. Apply Final Positions
        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, _laneIndex * laneOffset, laneSwitchSpeed * Time.deltaTime);
        pos.y = _y;
        pos.z = 0f;
        transform.position = pos;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();

        // Horizontal Lane Switching
        if (v.x > 0.5f && _prevMove.x <= 0.5f) ChangeLane(+1);
        else if (v.x < -0.5f && _prevMove.x >= -0.5f) ChangeLane(-1);

        // Vertical Jump Logic
        if (v.y > 0.5f && _prevMove.y <= 0.5f)
        {
            if (_jumpCount < MaxJumps)
            {
                _yVel = (_jumpCount == 0) ? jumpVelocity : doubleJumpVelocity;
                _jumpCount++;
            }
        }
        _prevMove = v;
    }

    private void ChangeLane(int delta)
    {
        int half = laneCount / 2;
        _laneIndex = Mathf.Clamp(_laneIndex + delta, -half, half);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // SAFETY CHECK: If we are significantly higher than the obstacle (on top), don't die
            // This assumes the obstacle's pivot/center is at its base.
            if (_y > other.bounds.max.y - 0.2f)
            {
                return;
            }

            Debug.Log("Obstacle hit confirmed!");
            GameManager.Instance.EndGame();
        }
    }
}