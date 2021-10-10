using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZomboTerrain
{
    public class OnSceneObjectInitializator
    {
        private List<IOnSceneObject> _onSceneObjects;      
        private List<BuffSample> _buffCollection;
        private RadarController _radarController;
        public OnSceneObjectInitializator(List<IOnSceneObject> onSceneObjects, RadarController radarController)
        {
            _onSceneObjects = onSceneObjects;
            _radarController = radarController;
            _buffCollection = Resources.Load<BuffBase>("DataBase/Buff Database").BuffSamples;
        }
        public List<IOnSceneObject> InitObjects()
        {
            BuffObjectsInit();
            RadarObjectsInit();

            return _onSceneObjects;
        }

        private void BuffObjectsInit()
        {
            foreach (IOnSceneObject onSceneObject in _onSceneObjects)
            {
                onSceneObject.ObjectRadarController = _radarController;

                if (onSceneObject is IBuff buffObject)
                {
                    for (int i = 0; i < _buffCollection.Count; i++)
                    {
                        if (_buffCollection[i].ID == buffObject.BuffID)
                        {
                            buffObject.ObjectBuff = new Buff(_buffCollection[i].ID, _buffCollection[i].BonusValue,
                                                                    _buffCollection[i].Duration, _buffCollection[i].Type);
                        }
                    }
                }
            }
        }
        private void RadarObjectsInit()
        {
            foreach (IObservableObject observableObject in _onSceneObjects)
            {
                if (observableObject is IBuff)
                    observableObject.RadarIcon = Resources.Load<Image>("BuffRadarObject");
            }
        }
    }
}