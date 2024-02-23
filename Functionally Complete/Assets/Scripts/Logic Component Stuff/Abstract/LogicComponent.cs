using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicComponent : MonoBehaviour
{
    private void Awake()
    {
        SetLayer(false);
    }

    public abstract void Logic();

    public abstract void ReDrawWires();

    public abstract void DeleteComponent();

    public void SetLayer(bool onState)
    {
        int layerNum = onState ? 0 : 6;
        gameObject.layer = layerNum;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layerNum;
        }
    }
}
