using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANDGate : ABYGate
{
    public override void Logic()
    {
        // a & b
        y.SetState(a.GetState() && b.GetState());
    }
}
