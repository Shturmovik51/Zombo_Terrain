using System.Collections.Generic;

namespace ZomboTerrain
{
    public interface IBuffTimerModel
    {
        public bool IsActive { get; }
        public List<Buff> ActiveBuffs { get; }

    }
}
