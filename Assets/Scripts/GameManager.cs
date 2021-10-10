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
        [SerializeField] private GameUIController _gameUIController;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private int _hitCollectionSize;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _timeSpeed;
        [SerializeField] private int _killsCountToWin;
        [SerializeField] private Transform _weaponPosition;
        [SerializeField] private Transform _flashLightPosition;
        [SerializeField] private Transform _shootEffectPosition;
        [SerializeField] private GameObject _shootEffects;
        [SerializeField] private Transform _directionalLight;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Data _data;
        [SerializeField] private PostProcessVolume _postProcessVolume;
        [SerializeField] private Transform _radarPosition;

        private List<IOnSceneObject> _onSceneObjects;
        private ControllersManager _controllersManager;
        private float _reloadTime;
        private void Start()
        {
            _onSceneObjects = FindObjectsOfType<MonoBehaviour>().OfType<IOnSceneObject>().ToList();

            _controllersManager = new ControllersManager();

            new GameInitializator(_controllersManager, _data, _playerView, _onSceneObjects, _gameUIController, _timeSpeed, 
                                    _directionalLight, this, _hitCollectionSize, _postProcessVolume, _radarPosition,
                                        _shootEffects, _reloadTime);

            _controllersManager.Initialization();

            GetReloadTime();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (Mathf.Approximately(Time.timeScale, 0))
                return;

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

        public void KillsCountDown()
        {
            _killsCountToWin--;

            if (_killsCountToWin == 0)
            {
                _gameUIController.StartEndGameScreen();
            }
        }
    }
}
