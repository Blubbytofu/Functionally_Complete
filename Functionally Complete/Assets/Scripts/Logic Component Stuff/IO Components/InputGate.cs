using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGate : LogicComponent, IConfigure
{
    [SerializeField] private OutputNode y;
    [SerializeField] private IndicatorLight statusLight;
    private bool state;

    public override void Logic()
    {
        // blank
    }

    public void Configure()
    {
        state = !state;
        y.SetState(state);
        statusLight.ChangeMat();
    }

    public override void ReDrawWires()
    {
        y.ReDrawWires();
    }

    public override void DeleteComponent()
    {
        y.DeleteWires();
    }
}
