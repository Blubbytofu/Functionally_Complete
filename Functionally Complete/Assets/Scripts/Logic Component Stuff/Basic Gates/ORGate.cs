public class ORGate : ABYGate
{
    public override void Logic()
    {
        // a | b
        y.SetState(a.GetState() || b.GetState());
    }
}
