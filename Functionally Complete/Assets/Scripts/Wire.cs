using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private OutputNode outputNode;
    [SerializeField] private InputNode inputNode;

    [SerializeField] private Material offMat;
    [SerializeField] private Material onMat;
    [SerializeField] private LineRenderer ln;

    public void DrawWire()
    {
        ln.SetPosition(0, this.outputNode.transform.position);
        ln.SetPosition(1, this.inputNode.transform.position);
    }

    public void CreateWire(OutputNode oN, InputNode iN)
    {
        // set iN and oN references
        this.outputNode = oN;
        this.inputNode = iN;

        // if inputNode already has a wire, remove and delete that wire
        Wire inputWire = iN.GetWire();
        if (inputWire != null)
        {
            inputWire.DeleteWire();
        }

        // add wire to outputNode and inputNode
        oN.AddWire(this);
        iN.SetWire(this);

        // draw the wire
        ln.positionCount = 2;
        DrawWire();

        // pass on the signal
        PassSignal();
    }

    public void PassSignal()
    {
        this.inputNode.SetState(outputNode.GetState());
        ln.material = this.outputNode.GetState() ? onMat : offMat;
    }

    public void DeleteWire()
    {
        this.outputNode.RemoveWire(this);
        this.inputNode.SetState(false);
        this.inputNode.SetWire(null);
        Destroy(gameObject);
    }
}
