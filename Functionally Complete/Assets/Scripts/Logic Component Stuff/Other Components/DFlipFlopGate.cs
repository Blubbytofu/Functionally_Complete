using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFlipFlopGate : LogicComponent
{
    [SerializeField] private InputNode d;
    [SerializeField] private InputNode clk;
    [SerializeField] private OutputNode q;
    [SerializeField] private OutputNode qBar;

    private bool logicPause;

    private void Start()
    {
        qBar.SetState(true);
    }

    public override void Logic()
    {
        if (!clk.GetState() || logicPause)
        {
            logicPause = false;
            return;
        }

        q.SetState(d.GetState());
        qBar.SetState(!q.GetState());
        logicPause = true;
    }
}
