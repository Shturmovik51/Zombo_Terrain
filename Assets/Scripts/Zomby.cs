using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zomby : MonoBehaviour
{
    public static Zomby instance;

    [SerializeField] private CharacterController charControl;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravityForse;
    [SerializeField] private int zombySpeed;
    [SerializeField] private int sensitivity;
    [SerializeField] private int jumpForse;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Animator zombyAnimator;


    private Health zombyHealth;
    public Transform CameraPos => cameraPos;
    public Health PlayerHealth => zombyHealth;

    private Vector3 gravitation;   
    private float xRotation;
    private bool isGrounded;
    private int shiftedSpeed;
    private bool isOnForwardMoove;
    private bool isOnBackwardMoove;
    private bool isOnLeftMoove;
    private bool isOnRightMoove;
    private bool isStanding;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        zombyHealth = GetComponent<Health>();
        zombyHealth.DeathEntity += ZombyDeath;
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
        ZombyMovement();
        ZombyLook();
        ZombyJump();

        if (Input.GetKeyDown(KeyCode.B))
            zombyHealth.TakeDamage(5);
    }

    private void ZombyMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundDetector.position, 0.3f, groundMask);

        shiftedSpeed = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftedSpeed = zombySpeed;
        }

        var moveDirection = transform.right * x + transform.forward * z;

        charControl.Move(moveDirection * (zombySpeed + shiftedSpeed) * Time.deltaTime);

        if (z != 0 || x != 0)
        {
            zombyAnimator.SetFloat("Zwalk", z);
            zombyAnimator.SetFloat("Xwalk", x);
            zombyAnimator.SetBool("isWalking", true);
        }
        
        //if (z > 0 && shiftedSpeed == 0)
        //{
        //    zombyAnimator.SetBool("isWalking", true);
        //    zombyAnimator.SetBool("isRunning", false);
        //}
        //if (z > 0 && shiftedSpeed == zombySpeed)
        //    zombyAnimator.SetBool("isRunning", true);
        //if (z == 0)
        //    zombyAnimator.SetBool("isWalking", false);


    }

    private void ZombyJump()
    {
        if (isGrounded)
        {
            gravitation.y = -2f;
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitation.y = Mathf.Sqrt(jumpForse * -2 * gravityForse);
        }

        gravitation.y += gravityForse * Time.deltaTime;

        charControl.Move(gravitation * Time.deltaTime);
    }

    private void ZombyLook()
    {
        var mouseLookX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var mouseLookY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseLookY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.Rotate(0f, mouseLookX, 0f);
        cameraPos.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void ZombyDeath()
    {
        Debug.Log("Умер!");
    }    
}
