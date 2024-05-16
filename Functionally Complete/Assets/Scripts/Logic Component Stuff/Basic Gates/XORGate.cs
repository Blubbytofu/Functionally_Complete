public class XORGate : ABYGate
{
    public override void Logic()
    {
        bool aState = a.GetState();
        bool bState = b.GetState();
        y.SetState((aState && !bState) || (!aState && bState));
    }
}
