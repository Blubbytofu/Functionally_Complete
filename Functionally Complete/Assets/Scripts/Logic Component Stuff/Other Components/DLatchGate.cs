using UnityEngine;

public class DLatchGate : LogicComponent
{
    [SerializeField] private InputNode d;
    [SerializeField] private InputNode en;
    [SerializeField] private OutputNode q;
    [SerializeField] private OutputNode qBar;

    private void Start()
    {
        qBar.SetState(true);
    }

    public override void Logic()
    {
        if (!en.GetState())
        {
            return;
        }

        q.SetState(d.GetState());
        qBar.SetState(!q.GetState());
    }
}
