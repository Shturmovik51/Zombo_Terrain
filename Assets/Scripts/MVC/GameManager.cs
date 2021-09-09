using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Player start parameters\n")]
    [SerializeField] private Transform playerStartPos;
    [SerializeField] private int palyerSpeed;
    [Tooltip("Множитель скорости перемещения")]
    [SerializeField] private int axeleration;
    [SerializeField] private int maxAmmoInMG;

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
        machineGun = new MachineGun(maxAmmoInMG, shootDamage, hitImpulseForce, weaponLightEffectsTime, wfxShootEffects, flashLight);
        //rifleGun

        weapon = machineGun;

        playerModel = new PlayerModel(palyerSpeed, weapon);
        playerModel.InitWeapon();
        playerView = Player.instance.GetComponent<PlayerView>();
        playerController = new PlayerController(playerView, playerModel);
        playerController.Enable();


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

        playerModel.PlayerLook(GetHorizontalAxis(), GetVerticalAxis());
        playerModel.PlayerMove(GetXAxis(), GetZAxis(), GetPlayerXDirection(), GetPlayerZDirection());
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

    private float GetXAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    private float GetZAxis()
    {
        return Input.GetAxis("Vertical");
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

    private float GetVerticalAxis()    {
        
        return Input.GetAxis("Mouse Y"); //mouseLookY
    }

}
