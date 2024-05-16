using UnityEngine;

public abstract class LogicComponent : MonoBehaviour
{
    protected LogicNode[] childrenNodes;
    [SerializeField] protected GameObject highlight;

    private void Awake()
    {
        this.SetLayer(false);
        this.GetAllChildNodes();
    }

    public abstract void Logic();

    public void ReDrawWires()
    {
        foreach (LogicNode node in childrenNodes)
        {
            node.ReDrawWires();
        }
    }

    public void DeleteComponent()
    {
        foreach (LogicNode node in childrenNodes)
        {
            node.DeleteWires();
        }
    }

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

    public void GetAllChildNodes()
    {
        LogicNode[] nodeList = GetComponentsInChildren<LogicNode>();
        childrenNodes = nodeList;
    }
}

/* Things to fix:
 * optimize clock code
 * add higher input gates
 * add 4 bit I/O
 * polish menu
 */