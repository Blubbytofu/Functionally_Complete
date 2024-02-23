using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NANDGate : ABYGate
{
    private void Start()
    {
        Logic();
    }

    public override void Logic()
    {
        // !(a & b)
        // !a | !b
        y.SetState(!a.GetState() || !b.GetState());
    }
}
