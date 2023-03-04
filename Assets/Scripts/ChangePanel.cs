using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for changing the current panel in the UI. Sits on every button responsible for changing panels
public class ChangePanel : MonoBehaviour
{
    public CanvasGroup CanvasGroupToOpen;
    public CanvasGroup CanvasGroupToClose;

    //Function responsible for changing canvas groups
    public void ChangeCanvas()
    {
        CanvasGroupToOpen.alpha = 1f;
        CanvasGroupToOpen.blocksRaycasts = true;
        CanvasGroupToClose.alpha = 0f;
        CanvasGroupToClose.blocksRaycasts = false;
    }
}
