using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;

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
    public Button QuestCompleteButton;

    [Header("Message")]
    public GameObject MessagePrefab;
    public GameObject MessageSpawnPoint;
    private Queue<string> messageQueue = new Queue<string>();
    private Coroutine queueChecker;
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

    public void AddToQueue(string message)
    {
        messageQueue.Enqueue(message);
        if(queueChecker == null)
        {
            queueChecker = StartCoroutine(CheckQueue());
        }
    }

    public GameObject SpawnMessage(string message)
    {
        GameObject go = Instantiate(MessagePrefab, MessageSpawnPoint.transform);
        go.GetComponent<Message>().SetMessage(message);
        return go;
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            GameObject prefab = SpawnMessage(messageQueue.Dequeue());
            do
            {
                yield return null;
            } while (!prefab.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Idle"));
        } while (messageQueue.Count > 0);
        queueChecker = null;
    }
}