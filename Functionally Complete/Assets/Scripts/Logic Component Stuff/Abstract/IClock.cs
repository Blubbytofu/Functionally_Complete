public interface IClock
{
    public abstract void OnClock(bool edge);
    public abstract ClockNode GetClkNode();
}
