using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AYGate : LogicComponent
{
    [SerializeField] protected InputNode a;
    [SerializeField] protected OutputNode y;

    public override abstract void Logic();
}
