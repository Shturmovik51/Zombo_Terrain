using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZomboTerrain
{
    public class HitEffectsController : IInitialisible, IController
    {
        private int _collectionSize;
        private Transform _parent;
        private Transform _bodyHitsContainer;
        private Transform _sandHitsContainer;
        private GameObject _bodyHitEffect;
        private GameObject _sandHitEffect;
        private List<GameObject> _bodyHitEffects;
        private List<GameObject> _sandHitEffects;

        public HitEffectsController(HitEffectsData hitEffectsData, int collectionSize, Transform parent)
        {
            _parent = parent;
            _collectionSize = collectionSize;
            _bodyHitEffect = hitEffectsData.BodyHitEffect;
            _sandHitEffect = hitEffectsData.SandHitEffect;

            _bodyHitEffects = new List<GameObject>(collectionSize);
            _sandHitEffects = new List<GameObject>(collectionSize);
        }

        public void Initialization()
        {
            _bodyHitsContainer = new GameObject(name: "BodyHitsContainer").transform;
            _sandHitsContainer = new GameObject(name: "SandHitsContainer").transform;

            _bodyHitsContainer.parent = _parent;
            _sandHitsContainer.parent = _parent;

            for (int i = 0; i < _collectionSize; i++)
            {
                InitHitCollection(_bodyHitEffect, _bodyHitEffects, _bodyHitsContainer);
                InitHitCollection(_sandHitEffect, _sandHitEffects, _sandHitsContainer);
            }
        }

        private void InitHitCollection(GameObject hitEffect, List<GameObject> hitCollection, Transform hitContainer)
        {
            var hit = Object.Instantiate(hitEffect, hitContainer.transform);
            hit.SetActive(false);
            hitCollection.Add(hit);
        }
    }
}
