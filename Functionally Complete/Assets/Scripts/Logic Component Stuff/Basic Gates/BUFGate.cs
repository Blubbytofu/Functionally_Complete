public class BUFGate : AYGate
{
    public override void Logic()
    {
        y.SetState(a.GetState());
    }
}
