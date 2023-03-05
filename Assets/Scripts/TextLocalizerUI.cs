using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextLocalizerUI : MonoBehaviour
{
    TMP_Text textField;

    public string key;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        textField = GetComponent<TMP_Text>();
        string value = LocalizationSystem.Instance.GetLocalizedValue(key);
        textField.text = value;
    }
}
