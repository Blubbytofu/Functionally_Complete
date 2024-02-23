using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XNORGate : ABYGate
{
    private void Start()
    {
        Logic();
    }

    public override void Logic()
    {
        bool aState = a.GetState();
        bool bState = b.GetState();
        y.SetState((!aState && !bState) || (aState && bState));
    }
}
