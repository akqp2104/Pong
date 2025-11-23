using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private void OnEnable()
    {
        eventSystem = EventSystem.current;
    }

    public void SetSelected(GameObject button)
    {
        eventSystem.SetSelectedGameObject(button);
    }
}
