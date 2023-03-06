using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("LocalizedTextElements")]
    public TextLocalizerUI[] TextLocalizerUIElements;

    [Header("QuestElements")]
    public GameObject QuestPrefab;
    public GameObject QuestList;
    #region Singleton
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public void QuitApp()
    {
        Application.Quit();
    }
}