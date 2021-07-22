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
    private float angle;
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
        if (Time.timeScale == 0)
            return;

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

        zombyAnimator.SetFloat("Zwalk", z);
        zombyAnimator.SetFloat("Xwalk", x);

        if(shiftedSpeed == zombySpeed)
            zombyAnimator.SetBool("isRunning", true);
        else
            zombyAnimator.SetBool("isRunning", false);

        if (z != 0 || x != 0)
            zombyAnimator.SetBool("isWalking", true);             
        else
            zombyAnimator.SetBool("isWalking", false);
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
        var mouseLookX = Input.GetAxis("Mouse X");
        var mouseLookY = Input.GetAxis("Mouse Y");

        xRotation -= mouseLookY * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.Rotate(0f, mouseLookX * sensitivity * Time.deltaTime, 0f);
        cameraPos.localRotation = Quaternion.Euler(xRotation, 0, 0);

        if (mouseLookX == 0)
            angle = Mathf.Lerp(angle,0,0.1f);
        if (mouseLookX > 0)
            if (angle < mouseLookX)
                angle = mouseLookX;        
        if (mouseLookX < 0)
            if (angle > mouseLookX)
                angle = mouseLookX;
       
        zombyAnimator.SetFloat("Rotation", angle);
        
    }

    private void ZombyDeath()
    {
        Debug.Log("����!");
    }    
}
