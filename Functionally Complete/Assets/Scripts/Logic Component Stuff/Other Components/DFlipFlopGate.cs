using System.Collections;
using UnityEngine;

public class DFlipFlopGate : LogicComponent, IClock
{
    [SerializeField] private InputNode d;
    [SerializeField] private ClockNode clk;
    [SerializeField] private OutputNode q;
    [SerializeField] private OutputNode qBar;

    private void Start()
    {
        qBar.SetState(true);
    }

    public override void Logic()
    {
        // blank
    }

    public void OnClock(bool edge)
    {
        if (edge)
        {
            q.SetState(d.GetState());
            qBar.SetState(!q.GetState());
        }
    }

    public ClockNode GetClkNode()
    {
        return this.clk;
    }
}
