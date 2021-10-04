using System.Collections.Generic;

public class BuffTimerModel
{    
    public bool IsActive { get; set; }
    //public Dictionary<Buff, Dictionary<BuffType, BuffMethods>> ActiveBuffs { get; set; }

    public List<Buff> ActiveBuffs;
    
}
