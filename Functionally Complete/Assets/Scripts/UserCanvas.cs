using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserCanvas : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster canvasRaycaster;
    private PointerEventData pointerData;
    [SerializeField] private GameObject[] menus;

    private void Awake()
    {
        ToggleMenu(1);
    }

    public void ClearScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool GetHudClick()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        canvasRaycaster.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            return true;
        }

        return false;
    }

    public void ToggleMenu(int num)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == num)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }
}
