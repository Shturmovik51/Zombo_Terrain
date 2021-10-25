using UnityEngine;

namespace ZomboTerrain
{
    public interface IBuff
    {
        public int BuffID { get; }
        public Renderer ObjectRenderer {get;}
        public Transform ObjectTransform { get; }
        public bool IsVisualised { get; set; }
        public Buff ThisObjectBuff { get; set; }
    }
}
