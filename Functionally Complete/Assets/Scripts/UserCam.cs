using UnityEngine;

public class UserCam : MonoBehaviour
{
    // singleton behavior
    public static UserCam instance { get; private set; }

    [SerializeField] private UserCanvas userCanvas;
    [SerializeField] private DotGrid dotGrid;
    [SerializeField] private Camera userCam, secondCam;
    [SerializeField] LineRenderer wireVis;

    // wire prefab
    [SerializeField] private GameObject wire;

    [SerializeField] private float zoomStep, maxZoom, minZoom;

    private Vector3 dragOrigin;
    private GameObject clickedObj, releasedObj;
    private bool overrideHUDState;

    private LogicNode nodeOne, nodeTwo;

    private LeftClickState leftClickState = LeftClickState.PAN;

    private enum LeftClickState
    {
        LOGIC_NODE,
        LOGIC_COMPONENT,
        PAN,
        HUD
    }

    private void Awake()
    {
        // check singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Zoom()
    {
        if ((Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.UpArrow)) && userCam.orthographicSize > maxZoom)
        {
            userCam.orthographicSize -= zoomStep;
            secondCam.orthographicSize = userCam.orthographicSize;
        }

        if ((Input.GetAxisRaw("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.DownArrow)) && userCam.orthographicSize < minZoom)
        {
            userCam.orthographicSize += zoomStep;
            secondCam.orthographicSize = userCam.orthographicSize;
        }
    }

    private void Configure()
    {
        // on Right Click down
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // if hud, do nothing
            if (userCanvas.GetHudClick())
            {
                return;
            }

            // else configure if able to
            GameObject obj = GetSelected();
            if (obj != null)
            {
                IConfigure con = obj.GetComponent<IConfigure>();
                if (con != null)
                {
                    con.Configure();
                }
            }
        }
    }

    private void Update()
    {
        Zoom();
        Configure();

        // on Left Click down //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // if click on hud
            if (userCanvas.GetHudClick())
            {
                leftClickState = LeftClickState.HUD;
                return;
            }

            // get obj
            clickedObj = GetSelected();

            // if no obj, pan
            if (clickedObj == null)
            {
                leftClickState = LeftClickState.PAN;
                dragOrigin = userCam.ScreenToWorldPoint(Input.mousePosition);
                return;
            }

            // if logic node, draw wire
            LogicNode logicNode = clickedObj.GetComponent<LogicNode>();
            if (logicNode != null)
            {
                // start wire
                leftClickState = LeftClickState.LOGIC_NODE;
                wireVis.positionCount = 2;
                logicNode.ClearWires();
                nodeOne = logicNode;
                Vector3 wireOnePos = nodeOne.transform.position;
                wireOnePos.z = 0;
                wireVis.SetPosition(0, wireOnePos);
                wireVis.SetPosition(1, wireOnePos);
                return;
            }

            // if logic component, drag and redraw wires
            LogicComponent logicComp = clickedObj.GetComponent<LogicComponent>();
            if (logicComp != null)
            {
                logicComp.SetLayer(true);
                logicComp.HighlightWires(true);
                leftClickState = LeftClickState.LOGIC_COMPONENT;
                dragOrigin = userCam.ScreenToWorldPoint(Input.mousePosition);
                dragOrigin -= clickedObj.transform.position;
            }
        }

        // on hold Left Click //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.Mouse0))
        {
            switch (leftClickState)
            {
                case LeftClickState.PAN:
                    dotGrid.UpdateGridPos();
                    Vector3 offset = userCam.ScreenToWorldPoint(Input.mousePosition) - dragOrigin;
                    userCam.transform.position -= offset;
                    break;
                case LeftClickState.LOGIC_NODE:
                    // draw wire vis
                    wireVis.SetPosition(1, new Vector3(userCam.ScreenToWorldPoint(Input.mousePosition).x, userCam.ScreenToWorldPoint(Input.mousePosition).y, 0));
                    break;
                case LeftClickState.LOGIC_COMPONENT:
                    clickedObj.transform.position = userCam.ScreenToWorldPoint(Input.mousePosition) - dragOrigin;
                    clickedObj.transform.position = new Vector3(clickedObj.transform.position.x, clickedObj.transform.position.y, 0);
                    clickedObj.GetComponent<LogicComponent>().ReDrawWires();
                    break;
                case LeftClickState.HUD:
                    if (overrideHUDState)
                    {
                        overrideHUDState = false;
                        leftClickState = LeftClickState.LOGIC_COMPONENT;
                    }
                    break;
            }
        }

        // on let go Left Click //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            switch (leftClickState)
            {
                case LeftClickState.LOGIC_NODE:
                    releasedObj = GetSelected();
                    // insane nesting
                    if (releasedObj != null && releasedObj.GetComponent<LogicNode>() != null)
                    {
                        nodeTwo = releasedObj.GetComponent<LogicNode>();
                        if (nodeTwo.GetParentComponent() != nodeOne.GetParentComponent())
                        {
                            if (nodeTwo.GetIOType() == 'O' && nodeOne.GetIOType() == 'I')
                            {
                                Wire w = Instantiate(wire, Vector3.zero, Quaternion.identity).GetComponent<Wire>();
                                w.CreateWire(nodeTwo.GetComponent<OutputNode>(), nodeOne.GetComponent<InputNode>());
                            }
                            else if (nodeOne.GetIOType() == 'O' && nodeTwo.GetIOType() == 'I')
                            {
                                Wire w = Instantiate(wire, Vector3.zero, Quaternion.identity).GetComponent<Wire>();
                                w.CreateWire(nodeOne.GetComponent<OutputNode>(), nodeTwo.GetComponent<InputNode>());
                            }
                        }
                    }
                    // end wire indicator
                    ResetWireVis();
                    break;
                case LeftClickState.LOGIC_COMPONENT:
                    LogicComponent logicComp = clickedObj.GetComponent<LogicComponent>();
                    logicComp.HighlightWires(false);

                    DeSelectLogicComp();

                    // of mouse is over menu, destroy component
                    if (userCanvas.GetHudClick())
                    {
                        // first remove all connections
                        logicComp.DeleteComponent();
                        // destroy
                        Destroy(clickedObj);
                    }
                    break;
            }
        }
    } // end of Update

    private GameObject GetSelected()
    {
        RaycastHit2D hit = Physics2D.Raycast(userCam.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        try
        {
            return hit.collider.gameObject;
        }
        catch
        {
            return null;
        }
    }

    private void DeSelectLogicComp()
    {
        if (clickedObj != null)
        {
            LogicComponent logicComp = clickedObj.GetComponent<LogicComponent>();
            if (logicComp != null)
            {
                logicComp.SetLayer(false);
                logicComp.HighlightWires(false);
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            // on lose focus deselect selected comp and reset wire vis
            DeSelectLogicComp();
            ResetWireVis();
        }
    }

    private void ResetWireVis()
    {
        wireVis.positionCount = 0;
        nodeOne = null;
        nodeTwo = null;
    }

    public void ForceHoldLogicComponent(GameObject obj)
    {
        // important //
        overrideHUDState = true;

        clickedObj = obj;
        LogicComponent logicComp = clickedObj.GetComponent<LogicComponent>();
        logicComp.HighlightWires(true);
        logicComp.SetLayer(true);
        dragOrigin = Vector3.zero;
    }

    public float GetMinZoom()
    {
        return minZoom;
    }
}
