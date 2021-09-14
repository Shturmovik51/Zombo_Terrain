using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate float GetXAxis();
    public delegate float GetZAxis();
    private GetXAxis xControl;
    private GetZAxis zControl;

    [SerializeField] private GameUI mainSceneUI;
    public GameUI MainSceneUI { get => mainSceneUI; set => mainSceneUI = value; }

    [Header("Player start parameters\n")]
    [SerializeField] private Transform playerStartPos;
    [SerializeField] private int palyerSpeed;
    [Tooltip("Множитель скорости перемещения")]
    [SerializeField] private int axeleration;
    [SerializeField] private int maxAmmoInMG;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private int jumpForce;
    
    private float xAxisValue;
    private float zAxisValue;

    [Header("Weapnos start parameters\n")]
    [SerializeField] private GameObject machineGunObj;
    [SerializeField] private Transform BodyHitsContainer;
    [SerializeField] private Transform SandHitsContainer;

    [SerializeField] private GameObject wfxBodyHit;
    [SerializeField] private GameObject wfxSandHit;
    [SerializeField] private GameObject wfxShootEffects;

    [SerializeField] private int hitsCountInCollection;
    [SerializeField] private int shootDamage;
    [SerializeField] private int hitImpulseForce;
    [SerializeField] private float weaponLightEffectsTime;
    [SerializeField] private Light flashLight;

    private PlayerModel playerModel;
    private PlayerView playerView;
    private PlayerController playerController;
    private Weapon weapon;
    private float reloadTime;





    public MachineGun machineGun;
    //public RifleGun rifleGun;




    #region Collections

    private Dictionary<GameObject, Health> healthContainer;       
    public Dictionary<GameObject, Health> HealthContainer {get => healthContainer;  set => healthContainer = value;}

    private List<GameObject> wfxBodyHits;
    public List<GameObject> WfxBodyHits { get => wfxBodyHits; set => wfxBodyHits = value; }

    private List<GameObject> wfxSandHits;
    public List<GameObject> WfxSandHits { get => wfxSandHits; set => wfxSandHits = value; }

    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
       
        healthContainer = new Dictionary<GameObject, Health>();
        WfxBodyHits = new List<GameObject>();
        WfxSandHits = new List<GameObject>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetReloadTime();        

        machineGun = new MachineGun(maxAmmoInMG, shootDamage, hitImpulseForce, weaponLightEffectsTime, reloadTime, wfxShootEffects, flashLight);
        //rifleGun
        weapon = machineGun;

        playerModel = new PlayerModel(palyerSpeed, jumpForce, weapon);
        playerView = Player.instance.GetComponent<PlayerView>();
        playerController = new PlayerController(playerView, playerModel);
        playerController.Enable();
        playerModel.EnableModel(playerController);


        for (int i = 0; i < hitsCountInCollection; i++)
        {
            InitHitCollection(wfxBodyHit, WfxBodyHits, BodyHitsContainer);
            InitHitCollection(wfxSandHit, WfxSandHits, SandHitsContainer);
        }
    }

    private void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0))
            return;

        if (Input.GetKeyDown(KeyCode.F))
            machineGun.FlashLightOnOff();

        if (Input.GetKey(KeyCode.Mouse0))
            playerModel.PLayerShoot();

        if (Input.GetKeyDown(KeyCode.R))
            playerModel.PLayerReloadWeapon();

        if (Input.GetButton("Jump"))
            playerModel.PlayerJump();

        playerModel.PlayerLook(GetHorizontalAxis(), GetVerticalAxis());
        playerModel.PlayerMove(xControl(), zControl(), GetPlayerXDirection(), GetPlayerZDirection());
        playerModel.Axeleration = Axeleration();
    }    

    private void InitHitCollection(GameObject wfxHit, List<GameObject> hitCollection, Transform hitContainer)
    {
        var hit = Instantiate(wfxHit, hitContainer.transform);
        hit.SetActive(false);
        hitCollection.Add(hit);
    }
    

    private Vector3 GetPlayerXDirection()
    {
        return playerView.transform.right;
    }

    private Vector3 GetPlayerZDirection()
    {
        return playerView.transform.forward;
    }

    public static float GetXControlWSAD()
    {
        return Input.GetAxis("Horizontal");
    }

    public static float GetZControlWSAD()
    {        
        return Input.GetAxis("Vertical");
    }

    public static float GetXAltControlWSAD()
    {
        return Input.GetAxis("AltHorizontal");
    }

    public static float GetZAltControlWSAD()
    {
        return Input.GetAxis("AltVertical");
    }

    public void GetControlWSAD()
    {
        xControl = GetXControlWSAD;
        zControl = GetZControlWSAD;
    }

    public void GetControl8546()
    {
        xControl = GetXAltControlWSAD;
        zControl = GetZAltControlWSAD;
    }

    private int Axeleration()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
            return 2;           
        else
            return 1;         
    }

    private float GetHorizontalAxis()
    {
        return Input.GetAxis("Mouse X"); //mouseLookX
    }

    private float GetVerticalAxis()    
    {        
        return Input.GetAxis("Mouse Y"); //mouseLookY
    }
    private void GetReloadTime()
    {
        var anims = playerAnimator.runtimeAnimatorController.animationClips;        

        foreach (var anim in anims)
        {
            if (anim.name == "Character_Reload")
               reloadTime = anim.length;              
        }
    }
}
