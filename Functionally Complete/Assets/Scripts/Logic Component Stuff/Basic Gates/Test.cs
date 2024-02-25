using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : LogicComponent
{
    [SerializeField] private InputNode a;
    [SerializeField] private InputNode b;
    [SerializeField] private InputNode c;
    [SerializeField] private OutputNode y;


    void Start()
    {
        Logic();
    }

    public override void Logic()
    {
        y.SetState(!a.GetState() || !b.GetState() || !c.GetState());
    }
}
