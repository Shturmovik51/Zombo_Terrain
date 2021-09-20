public class Buff
{
    public readonly BuffType type;
    public readonly int bonusValue;
    public readonly int duration;

    public Buff(BuffType type, int bonusValue, int duration)
    {
        this.type = type;
        this.bonusValue = bonusValue;
        this.duration = duration;
    }
}
