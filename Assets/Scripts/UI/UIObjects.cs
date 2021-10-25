using UnityEngine;

namespace ZomboTerrain
{
    [System.Serializable]
    public struct UIObjects
    {        
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _endGameScreen;

        public GameObject PausePanel => _pausePanel;
        public GameObject EndGameScreen => _endGameScreen;
    }
}

