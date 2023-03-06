using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("LocalizedTextElements")]
    public TextLocalizerUI[] TextLocalizerUIElements;

    [Header("CanvasGroups")]
    public CanvasGroup MainPanel;
    public CanvasGroup SettingsPanel;
    public CanvasGroup QuestCreationPanel;
    public CanvasGroup RequirementsPanel;
    public CanvasGroup QuestOverviewPanel;

    [Header("LevelingUI")]
    public Slider XpSlider;
    public TMP_Text LevelText;

    [Header("QuestElements")]
    public GameObject QuestPrefab;
    public GameObject QuestList;
    public TMP_Text QuestTitleText;
    public TMP_Text QuestRewardText;
    public TMP_Text QuestPunishmentText;
    public GameObject QuestOverviewRequirementPrefab;
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