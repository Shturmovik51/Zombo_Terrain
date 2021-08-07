using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private CharacterController charControl;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private Transform head;
    [SerializeField] private Transform weapon;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Light flashLight;
    [SerializeField] private float gravityForse;
    [SerializeField] private int playerSpeed;
    [SerializeField] private int sensitivity;
    [SerializeField] private int jumpForse;
    [SerializeField] private float shootDelayTime;
    [SerializeField] private int shootDamage;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject shootEffects;
    [SerializeField] private int hitImpulseForse;


    private Health playerHealth;
    public Health PlayerHealth => playerHealth;

    private Vector3 gravitation;
    private float xRotation;
    private bool isGrounded;
    private bool isShootDelay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        playerHealth = GetComponent<Health>();
        playerHealth.DeathEntity += PlayerDeath;
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
        if (Mathf.Approximately(Time.timeScale, 0))
            return;

        PlayerMovement();
        PlayerLook();

        if (Input.GetKeyDown(KeyCode.B))       
            playerHealth.TakeDamage(5);

        if (Input.GetKey(KeyCode.Mouse0) && !isShootDelay)
            PlayerShoot();

        if (Input.GetKeyDown(KeyCode.R))
            playerAnimator.SetBool("Reload", true);
        else
            playerAnimator.SetBool("Reload", false);


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!flashLight.enabled)
                flashLight.enabled = true;
            else if (flashLight.enabled)
                flashLight.enabled = false;
        }
    }
    
    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundDetector.position, 0.3f, groundMask);

        var moveDirection = (transform.right * x + transform.forward * z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= 2;
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }

        if (isGrounded)
        {
            gravitation.y = -2f;
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitation.y = Mathf.Sqrt(jumpForse * -2 * gravityForse);
        }

        gravitation.y += gravityForse * Time.deltaTime;

        charControl.Move(moveDirection * playerSpeed * Time.deltaTime);
        charControl.Move(gravitation * Time.deltaTime);

        playerAnimator.SetFloat("Movement", Mathf.Clamp01(moveDirection.magnitude));        
    }

    private void PlayerLook()
    {
        var mouseLookX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var mouseLookY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        xRotation -= mouseLookY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f); 

        transform.Rotate(0f, mouseLookX, 0f); 
        head.localRotation = Quaternion.Euler(xRotation, 0, 0);
        weapon.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void PlayerShoot()
    {
        isShootDelay = true;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                var mainGO = hit.collider.transform.root.gameObject;

                if (GameManager.instance.HealthContainer.ContainsKey(mainGO))
                {
                    var targethealth = GameManager.instance.HealthContainer[mainGO];
                    targethealth.TakeDamage(shootDamage);
                }

                var hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                hitRigidBody.AddForce(transform.forward * hitImpulseForse, ForceMode.Impulse);
            }

        playerAnimator.SetTrigger("Shoot");       
        StartCoroutine(ShootWFXandDelay());
    }

    private IEnumerator ShootWFXandDelay()
    {
        var flasfRot = shootEffects.transform.localRotation;
        flasfRot = Quaternion.Euler(flasfRot.x, flasfRot.y, Random.Range(0f, 360f));
        shootEffects.transform.localRotation = flasfRot;

        shootEffects.SetActive(true);

        yield return new WaitForSeconds(shootDelayTime);
        shootEffects.SetActive(false);
        isShootDelay = false;
        yield break;
    }

    private void PlayerDeath()
    {
        Debug.Log("Умер!");
    }
}
