using UnityEngine;

namespace ZomboTerrain
{
    public sealed class GameInitializator
    {
        public GameInitializator(ControllersManager controllers, Data data, PlayerView playerView, CollectableObject[] collectableObjects, 
                        GameUIController gameUIController, float timeSpeed, Transform directionalLight, Transform gameManagerTransform,
                        int hitCollectionSize)
        {
            Camera camera = Camera.main;
            var inputController = new InputController(data);
            var playerModel = new PlayerFactory(data.PlayerData).CreatePlayerModel();
            var playerController = new PlayerController(playerView, playerModel, inputController);
            var buffTimerModel = new BuffTimerModel();
            var buffTimerView = new BuffTimerView();
            var buffTimerController = new BuffTimerController(buffTimerModel, buffTimerView);
            var buffBehavior = new BuffBehavior(playerController, collectableObjects, buffTimerController);
            var gameWatchController = new GameWatchController(gameUIController, timeSpeed);
            var dailyCucleModel = new DailyCycleFactory(data.DailyCycleData).CreateDailyCycleModel();
            var dailyCycleController = new DailyCycleController(dailyCucleModel, gameWatchController, directionalLight);
            var hitEffectsController = new HitEffectsController(data.HitEffectsData, hitCollectionSize, gameManagerTransform);


            //var weapon = new MachineGun();

            // var playerInitialization = new PlayerInitialization(playerFactory, data.Player.Position);
            // var enemyFactory = new EnemyFactory(data.Enemy);
            // var enemyInitialization = new EnemyInitialization(enemyFactory);
            // controllers.Add(inputInitialization);



            controllers.Add(inputController);
            controllers.Add(playerController);
            controllers.Add(buffTimerController);
            controllers.Add(buffBehavior);
            controllers.Add(gameUIController);
            controllers.Add(gameWatchController);
            controllers.Add(dailyCycleController);
            controllers.Add(hitEffectsController);
            //controllers.Add(playerInitialization);
            //controllers.Add(enemyInitialization);
            //controllers.Add(new MoveController(inputInitialization.GetInput(), playerInitialization.GetPlayer(), data.Player));
            //controllers.Add(new EnemyMoveController(enemyInitialization.GetMoveEnemies(), playerInitialization.GetPlayer()));
            //controllers.Add(new CameraController(playerInitialization.GetPlayer(), camera.transform));
            //controllers.Add(new EndGameController(enemyInitialization.GetEnemies(), playerInitialization.GetPlayer().gameObject.GetInstanceID()));
        }


    }
}