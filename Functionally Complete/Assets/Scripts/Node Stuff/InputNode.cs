using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNode : LogicNode
{
    [SerializeField] private Wire wire;

    public void SetState(bool newState)
    {
        this.state = newState;
        parentLogicComp.Logic();
        SetMat();
    }

    public Wire GetWire()
    {
        return this.wire;
    }

    public void SetWire(Wire newW)
    {
        this.wire = newW;
    }

    public override char GetIOType()
    {
        return 'I';
    }

    public override void ClearWires()
    {
        if (this.wire != null)
        {
            this.wire.DeleteWire();
        }
    }

    public override void ReDrawWires()
    {
        if (this.wire != null)
        {
            this.wire.DrawWire();
        }
    }

    public override void DeleteWires()
    {
        this.ClearWires();
    }
}
