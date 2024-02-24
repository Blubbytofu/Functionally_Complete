using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class LogicNode : MonoBehaviour
{
    [SerializeField] protected LogicComponent parentLogicComp;
    [SerializeField] protected Material offMat;
    [SerializeField] protected Material onMat;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected bool state;

    public LogicComponent GetParentComponent()
    {
        return this.parentLogicComp;
    }

    private void Start()
    {
        SetMat();
    }

    protected void SetMat()
    {
        spriteRenderer.material = state ? onMat : offMat;
    }

    public bool GetState()
    {
        return this.state;
    }

    public abstract char GetIOType();
    // clear wires only on inputs
    public abstract void ClearWires();
    // delete wires on both
    public abstract void DeleteWires();
    public abstract void ReDrawWires();
    public abstract void HighlightWires(bool state);
}
