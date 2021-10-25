using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ZomboTerrain
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllers, Data data, PlayerView playerView, 
                            List<IOnSceneObject> _onSceneObjects, float timeSpeed, float timeJumpSpeed, 
                                WeaponElements weaponElements, GameManager gameManager, int hitCollectionSize, 
                                    PostProcessVolume postProcessVolume, Transform radarPosition, float reloadTime, 
                                        Camera radarCamera, int killsToWin, ZombieEnemy[] enemies, UIButtons uIButtons, 
                                            UIObjects uIObjects, UIFields uIFields, Light sunLight)
        {
            Camera camera = Camera.main;

            var radarCameraController = new RadarCamerController(radarCamera, playerView);
            var radarController = new RadarController(camera, radarPosition);
            var screenSaverController = new ScreenSaverController();
            var onSceneObjectInitializator = new OnSceneObjectInitializator(_onSceneObjects, radarController);
            var onSceneObjectController = new OnSceneObjectsController(onSceneObjectInitializator);
            var inputController = new InputController(data);
            var hitEffectsController = new HitEffectsController(data.HitEffectsData, hitCollectionSize, gameManager);
            var playerModel = new PlayerFactory(data.PlayerData).CreatePlayerModel();
            var weaponInitializator = new WeaponInitializator(data, reloadTime, gameManager, camera, 
                                                                hitEffectsController, weaponElements, inputController);
            var weaponController = new WeaponController(playerModel, weaponInitializator.InitWeapon());
            var endScreenController = new EndScreenController(uIButtons, uIObjects, killsToWin);
            var gamePanelController = new GamePanelController(uIFields, playerView, weaponController, endScreenController);
            var playerController = new PlayerController(playerView, playerModel, inputController);
            var buffTimerModel = new BuffTimerModel();
            var buffTimerView = new BuffTimerView();
            var buffTimerController = new BuffTimerController(buffTimerModel, buffTimerView);
            var buffBehavior = new BuffBehavior(playerController, _onSceneObjects, buffTimerController);
            var dailyCucleModel = new DailyCycleFactory(data.DailyCycleData, sunLight).CreateDailyCycleModel();
            var gameWatchController = new GameWatchController(timeSpeed, timeJumpSpeed, gamePanelController);
            var dailyCycleController = new DailyCycleController(dailyCucleModel, gameWatchController);
            var pausePanelController = new PausePanelController(uIButtons, uIObjects, inputController, dailyCycleController);
            var displayEffectController = new DisplayEffectController(camera, pausePanelController, postProcessVolume);
            var saveDataRepository = new SaveDataRepository(inputController, onSceneObjectController);
            var enemyController = new EnemyController(enemies, endScreenController);

            controllers.Add(radarCameraController);
            controllers.Add(screenSaverController);
            controllers.Add(radarController);
            controllers.Add(onSceneObjectController);
            controllers.Add(inputController);
            controllers.Add(hitEffectsController);
            controllers.Add(gamePanelController);
            controllers.Add(weaponController);
            controllers.Add(playerController);
            controllers.Add(buffTimerController);
            controllers.Add(buffBehavior);
            controllers.Add(pausePanelController);
            controllers.Add(gameWatchController);
            controllers.Add(dailyCycleController);
            controllers.Add(saveDataRepository);
            controllers.Add(displayEffectController);
            controllers.Add(endScreenController);
            controllers.Add(enemyController);
        }
    }
}