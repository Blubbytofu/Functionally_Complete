using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicComponent : MonoBehaviour
{
    protected LogicNode[] childrenNodes;
    [SerializeField] protected GameObject highlight;

    private void Awake()
    {
        SetLayer(false);
        childrenNodes = GetAllChildNodes();
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

    public void HighlightWires(bool state)
    {
        if (highlight != null)
        {
            highlight.SetActive(state);
        }

        if (childrenNodes.Length == 0)
        {
            return;
        }

        foreach (LogicNode node in childrenNodes)
        {
            node.HighlightWires(state);
        }
    }

    public LogicNode[] GetAllChildNodes()
    {
        LogicNode[] nodeList = GetComponentsInChildren<LogicNode>();
        return nodeList;
    }
}

/* Things to fix:
 * add latches and flipflops
 * add higher input gates
 * add 4 bit I/O
 * polish menu
 */