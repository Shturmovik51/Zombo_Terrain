using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    public class CollectableObject : MonoBehaviour, ICollectable, IObservableObject, IOnSceneObject, IBuff
    {
        [SerializeField] private int _buffID;
        private bool _isActive;
        public bool IsActive => _isActive;
        public RadarController ObjectRadarController { get; set; }
        public int BuffID => _buffID;
        
        public Action<Buff> OnApplyBuff;
        public Image RadarIcon { get; set; }
        public Buff ObjectBuff { get; set; }
        private void Awake()
        {
            _isActive = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            OnApplyBuff?.Invoke(ObjectBuff);
            SpecialDestroy();
        }

        public void ObjectActivation(bool condition)
        {
            _isActive = condition;
            gameObject.SetActive(condition);
        }

        public void AddObjectToRadar()
        {
            ObjectRadarController.RegisterRadarObject(gameObject, RadarIcon);
        }

        public void RemoveObjectFromRadar()
        {
            ObjectRadarController.RemoveRadarObject(gameObject);
        }

        public void SpecialDestroy()
        {
            RemoveObjectFromRadar();
            ObjectActivation(false);
        }
    }
}