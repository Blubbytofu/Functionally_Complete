using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUX2x1Gate : LogicComponent
{
    [SerializeField] private InputNode a;
    [SerializeField] private InputNode b;
    [SerializeField] private InputNode s;
    [SerializeField] private OutputNode y;

    public override void Logic()
    {
        if (!s.GetState())
        {
            y.SetState(a.GetState());
        }
        else
        {
            y.SetState(b.GetState());
        }
    }

    public override void ReDrawWires()
    {
        a.ReDrawWires();
        b.ReDrawWires();
        s.ReDrawWires();
        y.ReDrawWires();
    }

    public override void DeleteComponent()
    {
        a.DeleteWires();
        b.DeleteWires();
        s.DeleteWires();
        y.DeleteWires();
    }
}
