using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    public sealed class CollectableObject : MonoBehaviour, ICollectable, IObservableObject, IOnSceneObject, IBuff
    {
        [SerializeField] private int _buffID;
        public event Action<Buff> OnApplyBuff;
        public Renderer ObjectRenderer { get; private set; }
        public Transform ObjectTransform { get; private set; }
        public Image RadarIcon { get; set; }
        public Buff ThisObjectBuff { get; set; }
        public bool IsVisualised { get; set; }

        private bool _isActive;
        public bool IsActive => _isActive;
        public RadarController ObjectRadarController { get; set; }
        public int BuffID { get => _buffID; set => _buffID = value; }

        private void OnEnable()
        {
            ObjectRenderer = GetComponent<Renderer>();
            ObjectTransform = transform;
            _isActive = gameObject.activeInHierarchy;
        }

        private void OnDisable()
        {
            RemoveObjectFromRadar();
        }

        private void OnTriggerEnter(Collider other)
        {
            OnApplyBuff?.Invoke(ThisObjectBuff);
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