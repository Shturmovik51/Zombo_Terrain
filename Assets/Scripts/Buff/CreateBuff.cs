using UnityEngine;

namespace ZomboTerrain
{
    public sealed class CreateBuff : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        [Header("Enter Buff ID to create BuffObject with that Buff")]
        [SerializeField] private int _iD;
        [Header("Enter BuffObject height position relative to the ground")]
        [SerializeField] private Vector3 _offSet;
        public bool IsAcrive => _isActive;
        private Material _buffMaterial;
            
        public GameObject InstantiateBuff(Vector3 position)
        {
            var buffObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            buffObject.name = "BuffObject";
            buffObject.AddComponent<CollectableObject>().BuffID = _iD;
            buffObject.transform.parent = transform;
            buffObject.transform.position = position + _offSet;

            var buffCollection = Resources.Load<BuffBase>("DataBase/Buff Database").BuffSamples;
            _buffMaterial = null;

            for (int i = 0; i < buffCollection.Count; i++)
            {
                if (_iD == buffCollection[i].ID)                
                    _buffMaterial = buffCollection[i].BuffMaterial;                
            }

            if (_buffMaterial == null)
            {
                DestroyImmediate(buffObject);
                throw new System.Exception("No Buff with that ID");
            }
            else
            {
                buffObject.GetComponent<Renderer>().material = _buffMaterial;
                buffObject.GetComponent<Collider>().isTrigger = true;

                return buffObject;
            }            
        }
    }
}