using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private OutputNode outputNode;
    [SerializeField] private InputNode inputNode;

    [SerializeField] private Material offMat;
    [SerializeField] private Material onMat;
    [SerializeField] private Material selectedMat;
    [SerializeField] private LineRenderer wireRenderer;

    public void DrawWire()
    {
        this.wireRenderer.SetPosition(0, this.outputNode.transform.position);
        this.wireRenderer.SetPosition(1, this.inputNode.transform.position);
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
        this.wireRenderer.positionCount = 2;
        this.DrawWire();

        // pass on the signal
        this.PassSignal();
        this.inputNode.GetParentComponent().Logic();
    }

    //public void PassSignal()
    public void PassSignal()
    {
        this.inputNode.SetState(outputNode.GetState());
        this.wireRenderer.material = this.outputNode.GetState() ? onMat : offMat;
    }

    public void DeleteWire()
    {
        this.outputNode.RemoveWire(this);
        this.inputNode.SetState(false);
        this.inputNode.GetParentComponent().Logic();
        this.inputNode.SetWire(null);
        Destroy(gameObject);
    }

    public void HighlightWire(bool state)
    {
        int layerNum = state ? 0 : 6;
        gameObject.layer = layerNum;
        if (state)
        {
            this.wireRenderer.material = selectedMat;
        }
        else
        {
            this.wireRenderer.material = this.outputNode.GetState() ? onMat : offMat;
        }
    }

    public InputNode GetInputNode()
    {
        return this.inputNode;
    }
}
