using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DotGrid : MonoBehaviour
{
    [SerializeField] private GameObject dot;
    public Camera cam;

    private void Start()
    {
        Generate();
    }

    public void UpdateGridPos()
    {
        Vector3 pos = cam.transform.position;
        pos.x = Mathf.Ceil(pos.x);
        pos.y = Mathf.Ceil(pos.y);
        if (pos.x % 2 != 0)
        {
            pos.x++;
        }
        if (pos.y % 2 != 0)
        {
            pos.y++;
        }
        pos.z = 0;
        transform.position = pos;
    }

    private void Generate()
    {
        float minPossibleY = UserCam.instance.GetMinZoom();
        float minPossibleX = minPossibleY * cam.aspect;

        Vector3 dotPos = cam.transform.position;
        dotPos.x -= minPossibleX;
        dotPos.y -= minPossibleY;
        dotPos.z = 0;
        dotPos.x = Mathf.Ceil(dotPos.x);
        dotPos.y = Mathf.Ceil(dotPos.y);
        if (dotPos.x % 2 != 0)
        {
            dotPos.x++;
        }
        if (dotPos.y % 2 != 0)
        {
            dotPos.y++;
        }

        float firstX = dotPos.x;

        for (int j = 0; j < minPossibleY; j += 1)
        {
            for (int i = 0; i < minPossibleX; i += 1)
            {
                GameObject obj = Instantiate(dot, dotPos, Quaternion.identity);
                obj.transform.SetParent(transform);
                dotPos.x += 2;
            }
            dotPos.x = firstX;
            dotPos.y += 2;
        }
    }
}
