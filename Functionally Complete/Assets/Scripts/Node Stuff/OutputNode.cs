using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputNode : LogicNode
{
    // outputs can have multiple other inputs
    private List<Wire> wires = new List<Wire>();

    public void AddWire(Wire wire)
    {
        this.wires.Add(wire);
    }

    public void RemoveWire(Wire wire)
    {
        this.wires.Remove(wire);
    }

    private void SendSignal()
    {
        if (this.wires.Count == 0)
        {
            return;
        }

        List<LogicComponent> comps = new List<LogicComponent>();
        // first sends output signal to all input nodes
        // and tracks each unique logic component
        foreach (Wire w in wires)
        {
            LogicComponent logicComp = w.GetInputNode().GetParentComponent();
            if (!comps.Contains(logicComp))
            {
                comps.Add(logicComp);
            }
            w.PassSignal();
        }

        // then triggers logic on each logic component
        foreach(LogicComponent comp in comps)
        {
            comp.Logic();
        }
    }

    public void SetState(bool newState)
    {
        if (this.state != newState)
        {
            this.state = newState;
            SetMat();
            SendSignal();
        }
    }

    public override char GetIOType()
    {
        return 'O';
    }

    public override void ClearWires()
    {
        // blank
    }

    public override void ReDrawWires()
    {
        if (this.wires.Count == 0)
        {
            return;
        }

        foreach (Wire w in wires)
        {
            w.DrawWire();
        }
    }

    public override void DeleteWires()
    {
        if (this.wires.Count == 0)
        {
            return;
        }

        for (int i = 0; i < wires.Count; i++)
        {
            wires[i].DeleteWire();
            i--;
        }
    }

    public override void HighlightWires(bool state)
    {
        if (this.wires.Count == 0)
        {
            return;
        }

        foreach (Wire w in wires)
        {
            w.HighlightWire(state);
        }
    }
}
