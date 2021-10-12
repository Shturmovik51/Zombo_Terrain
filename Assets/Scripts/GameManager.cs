using System;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;

namespace ZomboTerrain
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _hitCollectionSize;
        [SerializeField] private int _killsCountToWin;
        [SerializeField] private float _timeSpeed;
        [SerializeField] private Data _data;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private Transform _flashLightPosition;
        [SerializeField] private Transform _shootEffectPosition;
        [SerializeField] private Transform _directionalLight;
        [SerializeField] private Transform _radarPosition;
        [SerializeField] private GameObject _shootEffects;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private PostProcessVolume _postProcessVolume;

        private float _reloadTime;
        private Camera[] _cameras;
        private ZombieEnemy[] _enemies;
        private ControllersManager _controllersManager;
        private List<IOnSceneObject> _onSceneObjects;
        private void Start()
        {
            GetReloadTime();

            _cameras = FindObjectsOfType<Camera>();
            _enemies = FindObjectsOfType<ZombieEnemy>();
            _controllersManager = new ControllersManager();
            _onSceneObjects = FindObjectsOfType<MonoBehaviour>().OfType<IOnSceneObject>().ToList();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            new GameInitializator(_controllersManager, _data, _playerView, _onSceneObjects, _gameUIController, _timeSpeed, 
                                    _directionalLight, this, _hitCollectionSize, _postProcessVolume, _radarPosition,
                                        _shootEffects, _reloadTime, _cameras, _killsCountToWin, _enemies);

            _controllersManager.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllersManager.LocalUpdate(deltaTime); 
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllersManager.LocalLateUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            _controllersManager.LocalLateUpdate(fixedDeltaTime);
        }

        private void OnDestroy()
        {
            _controllersManager.CleanUp();
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
    }
}
