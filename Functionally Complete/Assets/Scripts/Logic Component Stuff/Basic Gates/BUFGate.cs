using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUFGate : AYGate
{
    public override void Logic()
    {
        y.SetState(a.GetState());
    }
}
