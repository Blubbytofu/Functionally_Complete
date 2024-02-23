using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputGate : LogicComponent
{
    [SerializeField] private InputNode a;
    [SerializeField] private IndicatorLight statusLight;

    public override void Logic()
    {
        statusLight.ChangeMat(a.GetState());
    }

    public override void ReDrawWires()
    {
        a.ReDrawWires();
    }

    public override void DeleteComponent()
    {
        a.DeleteWires();
    }
}
