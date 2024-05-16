using UnityEngine;

public class FullADDERGate : LogicComponent
{
    [SerializeField] private InputNode a;
    [SerializeField] private InputNode b;
    [SerializeField] private InputNode cin;
    [SerializeField] private OutputNode sum;
    [SerializeField] private OutputNode cout;

    public override void Logic()
    {
        int numInputsOn = 0;
        if (a.GetState())
        {
            numInputsOn++;
        }
        if (b.GetState())
        {
            numInputsOn++;
        }
        if (cin.GetState())
        {
            numInputsOn++;
        }

        sum.SetState(numInputsOn == 1 || numInputsOn == 3);
        cout.SetState(numInputsOn > 1);
    }
}
