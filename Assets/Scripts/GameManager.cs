using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUI mainSceneUI;
    public GameUI MainSceneUI { get => mainSceneUI; set => mainSceneUI = value; }

    [Header("\nPlayer start parameters\n")]
    [SerializeField] private int _palyerSpeed;
    [SerializeField] private int _axeleration;
    [SerializeField] private int _maxAmmoInMachineGun;
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _startAmmoCount;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private GameObject _player;
    
    [Header("\nWeapnos start parameters\n")]
    [SerializeField] private int _hitsCountInCollection;
    [SerializeField] private int _shootDamage;
    [SerializeField] private int _hitImpulseForce;
    [SerializeField] private float _weaponLightEffectsTime;
    [SerializeField] private Light _flashLight;
    [SerializeField] private Transform _bodyHitsContainer;
    [SerializeField] private Transform _sandHitsContainer;
    [SerializeField] private GameObject _bodyHitEffect;
    [SerializeField] private GameObject _sandHitEffect;
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private Camera _mainCamera;

    [Header("\nDayCycle parameters\n")]
    [SerializeField] private float _dayRotationSpeed;
    [SerializeField] private float _timeJumpSpeed;
    [SerializeField] private float _cloudColorChangeSpeed;
    [SerializeField] private Color _sunSetCloudColor;
    [SerializeField] private Color _dayCloudColor;
    [SerializeField] private Material _cloudsMaterial;
    [SerializeField] private Transform _directionalLight;


    [HideInInspector] public DailyCycle dailyCycle;
    [HideInInspector] public MachineGun _machineGun;
    //[HideInInspector] public RifleGun rifleGun;

    private Weapon _weapon;
    private float _reloadTime;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private PlayerController _playerController;

    #region Collections       

    private List<GameObject> wfxBodyHits;
    public List<GameObject> WfxBodyHits { get => wfxBodyHits; set => wfxBodyHits = value; }

    private List<GameObject> wfxSandHits;
    public List<GameObject> WfxSandHits { get => wfxSandHits; set => wfxSandHits = value; }

    #endregion

    private void Awake()
    {       
        WfxBodyHits = new List<GameObject>();
        WfxSandHits = new List<GameObject>();
        dailyCycle = new DailyCycle(_dayRotationSpeed, _timeJumpSpeed, _cloudColorChangeSpeed, _dayCloudColor, 
                                    _sunSetCloudColor, _cloudsMaterial, _directionalLight);
    }        

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetReloadTime();        

        _machineGun = new MachineGun(_maxAmmoInMachineGun, _shootDamage, _hitImpulseForce, _weaponLightEffectsTime, _reloadTime, 
                                    _shootEffect, _flashLight, this, _mainCamera);
        //rifleGun
        _weapon = _machineGun;

        _playerModel = new PlayerModel(_palyerSpeed, _jumpForce, _weapon, _startAmmoCount, _axeleration);
        _playerView = _player.GetComponent<PlayerView>();
        _playerController = new PlayerController(_playerView, _playerModel);
        _playerController.Enable();


        for (int i = 0; i < _hitsCountInCollection; i++)
        {
            InitHitCollection(_bodyHitEffect, WfxBodyHits, _bodyHitsContainer);
            InitHitCollection(_sandHitEffect, WfxSandHits, _sandHitsContainer);
        }
    }

    private void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0))
            return;

        if (Input.GetKeyDown(KeyCode.F))
            _machineGun.FlashLightOnOff();

        if (Input.GetKey(KeyCode.Mouse0))
            _playerController.PLayerShoot();

        if (Input.GetKeyDown(KeyCode.R))
            _playerController.PLayerReloadWeapon();

        if (Input.GetButton("Jump"))
            _playerController.PlayerJump();

        _playerController.PlayerLook(GetHorizontalAxis(), GetVerticalAxis());
        _playerController.PlayerMove(GetInputAxis(), GetPlayerXDirection(), Axeleration());
    }
    private void FixedUpdate()
    {
        dailyCycle.DirectionLightRotation();
    }

    private void InitHitCollection(GameObject wfxHit, List<GameObject> hitCollection, Transform hitContainer)
    {
        var hit = Instantiate(wfxHit, hitContainer.transform);
        hit.SetActive(false);
        hitCollection.Add(hit);
    }
    
    private (float, float) GetInputAxis()
    {
        return (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private (Vector3, Vector3) GetPlayerXDirection()
    {
        return (_playerView.transform.right, _playerView.transform.forward);
    }
    
    private bool Axeleration()
    {
        return Input.GetKey(KeyCode.LeftShift);        
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
        var anims = _playerAnimator.runtimeAnimatorController.animationClips;        

        foreach (var anim in anims)
        {
            if (anim.name == "Character_Reload")
               _reloadTime = anim.length;              
        }
    }
}
