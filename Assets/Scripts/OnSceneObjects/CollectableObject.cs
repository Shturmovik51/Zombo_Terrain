using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    public class CollectableObject : MonoBehaviour, ICollectable, IObservableObject, IOnSceneObject, IBuff
    {
        [SerializeField] private int _buffID;

        public RadarController ObjectRadarController { get; set; }
        public int BuffID => _buffID;
        
        public Action<Buff> OnApplyBuff;
        public Image RadarIcon { get; set; }
        public Buff ObjectBuff { get; set; }        

        private void OnTriggerEnter(Collider other)
        {
            OnApplyBuff?.Invoke(ObjectBuff);
            SpecialDestroy();
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
            Destroy(gameObject);
        }
    }
}