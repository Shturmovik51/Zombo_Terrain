using System.Collections.Generic;

namespace ZomboTerrain
{
    public sealed class BuffTimerModel
    {
        public bool IsActive { get; set; }

        public List<Buff> ActiveBuffs;
    }
}