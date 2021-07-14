using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private CharacterController charControl;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravityForse;
    [SerializeField] private int playerSpeed;
    [SerializeField] private int sensitivity;
    [SerializeField] private int jumpForse;

    private Health playerHealth;
    public Health PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }

    private Vector3 gravitation;
    private Vector3 oldPos;
    private Vector3 newPos;
    private float xRotation;
    private bool isGrounded;
    private int shiftedSpeed;
    private int playerVelosity;

    private void Awake()
    {
        instance = this;
        PlayerHealth = GetComponent<Health>();
    }
    void Start()
    {
        isGrounded = false;
        xRotation = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMovement();
        PlayerLook();

        if (Input.GetKeyDown(KeyCode.B))
        {
            playerHealth.TakeDamage(5);
        }
    }
    
    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        oldPos = transform.position;
        isGrounded = Physics.CheckSphere(groundDetector.position, 0.3f, groundMask);

        shiftedSpeed = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftedSpeed = playerSpeed;
        }

        var moveDirection = transform.right * x + transform.forward * z;

        if (isGrounded)
        {
            gravitation.y = -2f;
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitation.y = Mathf.Sqrt(jumpForse * -2 * gravityForse);
        }

        gravitation.y += gravityForse * Time.deltaTime;

        charControl.Move(moveDirection * (playerSpeed + shiftedSpeed) * Time.deltaTime);
        charControl.Move(gravitation * Time.deltaTime);

        newPos = transform.position;
        playerVelosity = (int)(Vector3.Magnitude(newPos - oldPos) / Time.deltaTime);
    }

    private void PlayerLook()
    {
        var mouseLookX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var mouseLookY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        xRotation -= mouseLookY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.Rotate(0f, mouseLookX, 0f);
        head.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
