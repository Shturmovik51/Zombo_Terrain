namespace ZomboTerrain
{
    public sealed class EnemyController : IInitialisible, IController
    {
        public EndScreenController _endScreenController;
        private ZombieEnemy[] _enemies;

        public EnemyController(ZombieEnemy[] enemies, EndScreenController endScreenController)
        {
            _enemies = enemies;
            _endScreenController = endScreenController;
        }

        public void Initialization()
        {
            foreach (var enemy in _enemies)
            {
                enemy.OnEnemyDeath += _endScreenController.KillsCountDown;
            }
        }
    }
}