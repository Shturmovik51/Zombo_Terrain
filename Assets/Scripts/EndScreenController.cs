using UnityEngine;

namespace ZomboTerrain
{
    public sealed class EndScreenController : IController
    {
        private GameUIController _gameUIController;
        private int _killsCountToWin;

        public EndScreenController(GameUIController gameUIController, int killsCountToWin)
        {
            _gameUIController = gameUIController;
            _killsCountToWin = killsCountToWin;
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
