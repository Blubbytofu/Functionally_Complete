using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABYGate : LogicComponent
{
    [SerializeField] protected InputNode a;
    [SerializeField] protected InputNode b;
    [SerializeField] protected OutputNode y;

    public override abstract void Logic();

    public override void ReDrawWires()
    {
        a.ReDrawWires();
        b.ReDrawWires();
        y.ReDrawWires();
    }

    public override void DeleteComponent()
    {
        a.DeleteWires();
        b.DeleteWires();
        y.DeleteWires();
    }
}
