using UnityEngine;

public class OutputGate : LogicComponent
{
    [SerializeField] private InputNode a;
    [SerializeField] private IndicatorLight statusLight;

    public override void Logic()
    {
        statusLight.ChangeMat(a.GetState());
    }
}
