using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INVGate : AYGate
{
    private void Start()
    {
        Logic();
    }

    public override void Logic()
    {
        y.SetState(!a.GetState());
    }
}
