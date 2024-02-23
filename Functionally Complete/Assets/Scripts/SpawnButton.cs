using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject prefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject obj = Instantiate(prefab, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
        UserCam.instance.ForceHoldLogicComponent(obj);
    }
}
