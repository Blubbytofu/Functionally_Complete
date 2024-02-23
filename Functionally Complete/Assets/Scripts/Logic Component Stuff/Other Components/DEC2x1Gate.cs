using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEC2x1Gate : LogicComponent
{
    [SerializeField] private InputNode en;
    [SerializeField] private InputNode s;
    [SerializeField] private OutputNode y0;
    [SerializeField] private OutputNode y1;

    public override void Logic()
    {
        if (!en.GetState())
        {
            y0.SetState(false);
            y1.SetState(false);
        }
        else
        {
            bool sState = s.GetState();
            y0.SetState(!sState);
            y1.SetState(sState);
        }
    }

    public override void ReDrawWires()
    {
        en.ReDrawWires();
        s.ReDrawWires();
        y0.ReDrawWires();
        y1.ReDrawWires();
    }

    public override void DeleteComponent()
    {
        en.DeleteWires();
        s.DeleteWires();
        y0.DeleteWires();
        y1.DeleteWires();
    }
}
