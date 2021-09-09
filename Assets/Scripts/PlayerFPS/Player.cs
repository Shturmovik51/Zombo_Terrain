using System.Collections;
using TMPro;
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
    [SerializeField] private int ammoCount;
    [SerializeField] private int maxWeaponMagazineCount;
    [SerializeField] private GameObject wfxSandHit;
    [SerializeField] private GameObject wfxBodyHit;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI ammoMagazineText;

    private AnimationClip reloadAnim;
    private Health playerHealth;
    public Health PlayerHealth => playerHealth;

    private Coroutine shootDelay;
    private Coroutine speedBuffTimeDuration;
    private Coroutine jumpBuffTimeDuration;
    private int ammoMagazineCount;
    private int buffedSpeed = 0;
    private int buffedJump = 0;
    private Vector3 gravitation;
    private float verticalRotation = 0f;
    public bool isGrounded;
    private bool isShootDelay;
    private bool isReloading;
    private IPlayerMove iplayerMove;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var anims = playerAnimator.runtimeAnimatorController.animationClips;
        foreach (var anim in anims)
        {
            if (anim.name == "Character_Reload")
                reloadAnim = anim;
        }

        ammoMagazineCount = maxWeaponMagazineCount;
        RefreshAmmoUI();        
    }

    void Update()
    {
        //if (Mathf.Approximately(Time.timeScale, 0))
        //    return;
        //iplayerMove.PlayerMove(playerSpeed + buffedSpeed);
        //PlayerJump();
        //PlayerLook();

        if (Input.GetKeyDown(KeyCode.B))       
            playerHealth.TakeDamage(5);

        //if (Input.GetKey(KeyCode.Mouse0) && !isShootDelay && !isReloading)
        //    PlayerShoot();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            PlayerReload();

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    flashLight.enabled = !flashLight.enabled;
        //}
    }

    public void SetWSADcontrol()
    {
        iplayerMove = GetComponent<PlayerMoveWSAD>();
    }
    public void Set8546control()
    {
        iplayerMove = GetComponent<PlayerMove8546>();
    }

    private void PlayerJump()
    {
        isGrounded = Physics.CheckSphere(groundDetector.position, 0.3f, groundMask);

        if (isGrounded)
        {
            gravitation.y = -2f;
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitation.y = Mathf.Sqrt((jumpForse + buffedJump) * -2 * gravityForse);
        }

        gravitation.y += gravityForse * Time.deltaTime;
      
        charControl.Move(gravitation * Time.deltaTime);       
    }

    private void PlayerLook()
    {
        var mouseLookX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var mouseLookY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        verticalRotation -= mouseLookY;
        verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f); 

        transform.Rotate(0f, mouseLookX, 0f); 
        head.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        weapon.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void PlayerShoot()
    {
        ammoMagazineCount--;

        if (ammoMagazineCount < 0)
        {
            ammoMagazineCount = 0;
            PlayerReload();
            return;
        }

        RefreshAmmoUI();
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

                Instantiate(wfxBodyHit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                var hitRigidBody = hit.collider.GetComponent<Rigidbody>();
                hitRigidBody.AddForce(transform.forward * hitImpulseForse, ForceMode.Impulse);
            }
            else
            {                         
                Instantiate(wfxSandHit, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }

        playerAnimator.SetBool("Shoot", true);       
        
        if(shootDelay == null)
            StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        var flashRot = shootEffects.transform.localRotation;
        flashRot = Quaternion.Euler(flashRot.x, flashRot.y, Random.Range(0f, 360f));
        shootEffects.transform.localRotation = flashRot;

        shootEffects.SetActive(true);

        yield return new WaitForSeconds(shootDelayTime);
        playerAnimator.SetBool("Shoot", false);
        isShootDelay = false;
        shootEffects.SetActive(false);
        yield break;
    }

    private void PlayerReload()
    {
        if (ammoCount == 0)
            return;

        isReloading = true;
        playerAnimator.SetTrigger("Reload");
        StartCoroutine(ReloadTimer());
    }

    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadAnim.length);

        if (ammoCount < maxWeaponMagazineCount)
        {
            ammoMagazineCount = ammoCount;
            ammoCount -= ammoMagazineCount;
        }
        else
        {
            var ammoNeeded = maxWeaponMagazineCount - ammoMagazineCount;
            ammoMagazineCount = maxWeaponMagazineCount;
            ammoCount -= ammoNeeded;
        }

        RefreshAmmoUI();
        isReloading = false;
    }

    private void RefreshAmmoUI()
    {
        ammoText.text = ammoCount.ToString();
        ammoMagazineText.text = ammoMagazineCount.ToString();
    }

    private void PlayerDeath()
    {
        Debug.Log("Умер!");
    }

    public void AddBuff(Buff buff)
    {
        if(buff.type == BuffType.Speed)
        {
            buffedSpeed += buff.additiveBonus;           
            if (speedBuffTimeDuration == null)
                speedBuffTimeDuration = StartCoroutine(BuffTimeDuration(buff));
        }

        if(buff.type == BuffType.Jump)
        {
            buffedJump += buff.additiveBonus;
            if (jumpBuffTimeDuration == null)
                jumpBuffTimeDuration = StartCoroutine(BuffTimeDuration(buff));
        }

        if(buff.type == BuffType.Health)        
            playerHealth.HealthUp(buff.additiveBonus);
        
        if (buff.type == BuffType.Ammo)
            ammoCount += buff.additiveBonus;
    }

    private IEnumerator BuffTimeDuration(Buff buff)
    {
        yield return new WaitForSeconds(buff.duration);
        RemoveBuff(buff);
    }
    public void RemoveBuff(Buff buff)
    {
        if (buff.type == BuffType.Speed)
        {
            buffedSpeed = 0;
        }
        if (buff.type == BuffType.Jump)
        {
            buffedJump = 0;
        }
    }
}
