using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControls playerControls;
    Rigidbody rigidBody;
    [SerializeField] float moveSpeed = 10f;

    // Define the boundaries for clamping
    [SerializeField] float xMin = -4f;
    [SerializeField] float xMax = 4f;
    [SerializeField] float zMin = -2f;
    [SerializeField] float zMax = 2f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject!");
        }
        rigidBody.isKinematic = true; // Ensure the Rigidbody is kinematic for direct movement
    }

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        Debug.Log(moveInput);

        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); 
        Vector3 newPosition = transform.position + moveDirection * Time.fixedDeltaTime * moveSpeed;

        newPosition = ClampPosition(newPosition);

        rigidBody.MovePosition(newPosition);
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(position.z, zMin, zMax);
        
        return new Vector3(clampedX, position.y, clampedZ);
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
}