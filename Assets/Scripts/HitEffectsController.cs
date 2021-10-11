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

        public HitEffectsController(HitEffectsData hitEffectsData, int collectionSize, GameManager parent)
        {
            _parent = parent.transform;
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

        public GameObject GetBodyHitEffectFromPool()
        {
            var effect = _bodyHitEffects[0];
            _bodyHitEffects.Remove(effect);
            //EffectTimer(effect, _bodyHitEffects).StartThisCoroutine();
            return effect;
        }

        public GameObject GetSandHitEffectFromPool()
        {
            var effect = _sandHitEffects[0];
            _sandHitEffects.Remove(effect);
            //EffectTimer(effect, _sandHitEffects).StartThisCoroutine();
            return effect;
        }

        private IEnumerator EffectTimer(GameObject effect, List<GameObject> effectPool)
        {
            yield return new WaitForSeconds(5);
            ReturnHitEffectToPool(effect, effectPool);
        }

        private void ReturnHitEffectToPool(GameObject effect, List<GameObject> effectPool)
        {
            effect.transform.parent = _parent;
            effectPool.Add(effect);
        }
    }
}
