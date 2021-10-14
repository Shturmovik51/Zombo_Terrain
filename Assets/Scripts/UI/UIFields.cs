using TMPro;
using UnityEngine;

namespace ZomboTerrain
{
    [System.Serializable]
    public struct UIFields
    {      
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private TextMeshProUGUI _ammoMagazineText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _killsText;

        public TextMeshProUGUI AmmoText => _ammoText;
        public TextMeshProUGUI AmmoMagazineText => _ammoMagazineText;
        public TextMeshProUGUI TimeText => _timeText;
        public TextMeshProUGUI KillsText => _killsText;
    }
}

