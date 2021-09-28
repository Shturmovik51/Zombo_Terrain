using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUI _mainSceneUI;
    public GameUI MainSceneUI { get => _mainSceneUI; set => _mainSceneUI = value; }

    [Header("\nPlayer start parameters\n")]
    [SerializeField] private int _palyerSpeed;
    [SerializeField] private int _axeleration;
    [SerializeField] private int _maxAmmoInMachineGun;
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _startAmmoCount;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private GameObject _player;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private PlayerController _playerController;
    
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
    //[HideInInspector] public RifleGun rifleGun;
    [HideInInspector] public MachineGun _machineGun;
    private Weapon _weapon;
    private float _reloadTime;

    [Header("\nDayCycle parameters\n")]
    [SerializeField] private float _dayRotationSpeed;
    [SerializeField] private float _timeJumpSpeed;
    [SerializeField] private float _cloudColorChangeSpeed;
    [SerializeField] private Color _sunSetCloudColor;
    [SerializeField] private Color _dayCloudColor;
    [SerializeField] private Material _cloudsMaterial;
    [SerializeField] private Transform _directionalLight;

    [Header("\nBuffTimer parameters\n")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _timeSpeed;
    private BuffTimerController _buffTimerController;
    private BuffTimerModel _buffTimerModel;
    private BuffTimerView _buffTimerView;

    [Header("\nOther\n")]
    [SerializeField] private int _killsCountToWin;
    private DailyCycle _dailyCycle;
    private InGameWatch _gameWatch;
    private float _deltaTime;

    #region Collections       

    [HideInInspector] public List<GameObject> BodyHitEffects;
    [HideInInspector] public List<GameObject> SandHitEffects;

    #endregion

    private void Awake()
    {      
        _gameWatch = new InGameWatch(_timerText, _timeSpeed);
        BodyHitEffects = new List<GameObject>();
        SandHitEffects = new List<GameObject>();
        _dailyCycle = new DailyCycle(_timeJumpSpeed, _cloudColorChangeSpeed, _dayCloudColor, 
                                    _sunSetCloudColor, _cloudsMaterial, _directionalLight, _gameWatch);
    }        

    private void Start()
    {
        _buffTimerModel = new BuffTimerModel();
        _buffTimerView = new BuffTimerView();
        _buffTimerController = new BuffTimerController(_buffTimerModel, _buffTimerView);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetReloadTime();        

        _machineGun = new MachineGun(_maxAmmoInMachineGun, _shootDamage, _hitImpulseForce, _weaponLightEffectsTime, _reloadTime, 
                                    _shootEffect, _flashLight, this, _mainCamera);
        //rifleGun

        _weapon = _machineGun;

        _playerModel = new PlayerModel(_palyerSpeed, _jumpForce, _weapon, _startAmmoCount, _axeleration);
        _playerView = _player.GetComponent<PlayerView>();
        _playerController = new PlayerController(_playerView, _playerModel, _buffTimerController);
        _playerController.Enable();
        _dailyCycle.Enable();

        for (int i = 0; i < _hitsCountInCollection; i++)
        {
            InitHitCollection(_bodyHitEffect, BodyHitEffects, _bodyHitsContainer);
            InitHitCollection(_sandHitEffect, SandHitEffects, _sandHitsContainer);
        }
    }

    private void Update()
    {
        _gameWatch.TimeCountDown();
        _buffTimerController.LocalUpdate();

        if (Mathf.Approximately(Time.timeScale, 0))
            return;

        if (Input.GetKeyDown(KeyCode.F))
            _machineGun.FlashLightOnOff();

        if (Input.GetKey(KeyCode.Mouse0))
            _playerController.PLayerShoot();

        if (Input.GetKeyDown(KeyCode.R))
            _playerController.PLayerReloadWeapon();

        if (Input.GetButton(AxisManager.Jump))
            _playerController.PlayerJump();

        _playerController.PlayerLook(GetAxis());
        _playerController.PlayerMove(GetInputAxis(), GetPlayerXDirection(), Axeleration());
    }    

    private void InitHitCollection(GameObject wfxHit, List<GameObject> hitCollection, Transform hitContainer)
    {
        var hit = Instantiate(wfxHit, hitContainer.transform);
        hit.SetActive(false);
        hitCollection.Add(hit);
    }
    
    private (float, float) GetInputAxis()
    {
        return (Input.GetAxis(AxisManager.Horizontal), Input.GetAxis(AxisManager.Vertical));
    }

    private (Vector3, Vector3) GetPlayerXDirection()
    {
        return (_playerView.transform.right, _playerView.transform.forward);
    }
    
    private bool Axeleration()
    {
        return Input.GetKey(KeyCode.LeftShift);        
    }

    private (float, float) GetAxis()
    {
        return (Input.GetAxis(AxisManager.MouseX), Input.GetAxis(AxisManager.MouseY));
    }

    private void GetReloadTime()
    {
        var anims = _playerAnimator.runtimeAnimatorController.animationClips;        

        foreach (var anim in anims)
        {
            if (anim.name == "Character_Reload")
            {
                _reloadTime = anim.length;
            }
        }

        if (_reloadTime <= 0) throw new Exception("Wrong ReloadTimer value determination in GetReloadTime()");
    }

    public void KillsCountDown()
    {
        _killsCountToWin--;

        if(_killsCountToWin == 0)
        {
            _mainSceneUI.StartEndGameScreen();
        }
    }
}
