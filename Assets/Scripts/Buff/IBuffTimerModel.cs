
using System.Collections.Generic;

public interface IBuffTimerModel
{
    public bool IsActive { get; }
    public List<Buff> ActiveBuffs { get; }
}
