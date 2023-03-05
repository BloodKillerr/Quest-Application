using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public CanvasGroup CanvasGroupToOpen;
    public CanvasGroup CanvasGroupToClose;

    public void ChangeCanvas()
    {
        CanvasGroupToOpen.alpha = 1f;
        CanvasGroupToOpen.blocksRaycasts = true;
        CanvasGroupToClose.alpha = 0f;
        CanvasGroupToClose.blocksRaycasts = false;
    }
}
