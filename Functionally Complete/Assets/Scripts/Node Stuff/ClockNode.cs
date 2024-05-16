using UnityEngine;

public class ClockNode : InputNode 
{
    [SerializeField] private bool detectPositiveEdge;
    [SerializeField] private bool detectNegativeEdge;
        
    public new void SetState(bool newState)
    {
        base.SetState(newState);
        if (detectPositiveEdge && this.state)
        {
            parentLogicComp.GetComponent<IClock>().OnClock(true);
        }
        else if (detectNegativeEdge && !this.state)
        {
            parentLogicComp.GetComponent<IClock>().OnClock(false);
        }
    }
}
