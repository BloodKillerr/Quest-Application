using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public TMP_Text MessageText;

    private void Start()
    {
        DestroyAfterTime(1.6f);
    }

    public void SetMessage(string text)
    {
        MessageText.text = text;
    }

    public void DestroyAfterTime(float delay)
    {
        Destroy(gameObject, delay);
    }
}
