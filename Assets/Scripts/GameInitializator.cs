using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ZomboTerrain
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllers, Data data, PlayerView playerView, List<IOnSceneObject> _onSceneObjects, 
                        GameUIController gameUIController, float timeSpeed, Transform directionalLight, GameManager gameManager,
                        int hitCollectionSize, PostProcessVolume postProcessVolume, Transform radarPosition, GameObject shootEffects,
                        float reloadTime, Camera[] cameras)
        {
            Camera camera = Camera.main;

            var radarController = new RadarController(camera, radarPosition);
            var screenSaverController = new ScreenSaverController(cameras);
            var onSceneObjectInitializator = new OnSceneObjectInitializator(_onSceneObjects, radarController);
            var onSceneObjectController = new OnSceneObjectsController(onSceneObjectInitializator.InitObjects());
            var inputController = new InputController(data);
            var hitEffectsController = new HitEffectsController(data.HitEffectsData, hitCollectionSize, gameManager);
            var playerModel = new PlayerFactory(data.PlayerData).CreatePlayerModel();
            var weaponInitializator = new WeaponInitializator(data, reloadTime, gameManager, camera, hitEffectsController, shootEffects);
            var weaponController = new WeaponController(playerModel, weaponInitializator.InitWeapon());
            var playerController = new PlayerController(playerView, playerModel, inputController);
            var buffTimerModel = new BuffTimerModel();
            var buffTimerView = new BuffTimerView();
            var buffTimerController = new BuffTimerController(buffTimerModel, buffTimerView);
            var buffBehavior = new BuffBehavior(playerController, _onSceneObjects, buffTimerController);
            var gameWatchController = new GameWatchController(gameUIController, timeSpeed);
            var dailyCucleModel = new DailyCycleFactory(data.DailyCycleData).CreateDailyCycleModel();
            var dailyCycleController = new DailyCycleController(dailyCucleModel, gameWatchController, directionalLight);
            var displayEffectController = new DisplayEffectController(camera, gameUIController, postProcessVolume);
            var saveDataRepository = new SaveDataRepository(inputController, onSceneObjectController);


            controllers.Add(screenSaverController);
            controllers.Add(radarController);
            controllers.Add(onSceneObjectController);
            controllers.Add(inputController);
            controllers.Add(hitEffectsController);
            controllers.Add(weaponController);
            controllers.Add(playerController);
            controllers.Add(buffTimerController);
            controllers.Add(buffBehavior);
            controllers.Add(gameUIController);
            controllers.Add(gameWatchController);
            controllers.Add(dailyCycleController);
            controllers.Add(saveDataRepository);
            controllers.Add(displayEffectController);
        }
    }
}