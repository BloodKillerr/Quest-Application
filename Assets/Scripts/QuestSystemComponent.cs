using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSystemComponent : MonoBehaviour
{
    public List<Quest> CurrentQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        foreach(Quest currentQuest in CurrentQuests)
        {
            if(currentQuest.Title == quest.Title)
            {
                return;
            }
        }
        CurrentQuests.Add(quest);

        GameObject go = Instantiate(UIManager.Instance.QuestPrefab, UIManager.Instance.QuestList.transform);
        go.name = quest.Title;
        ChangePanel changePanel = go.GetComponent<ChangePanel>();
        changePanel.CanvasGroupToOpen = UIManager.Instance.QuestOverviewPanel;
        changePanel.CanvasGroupToClose = UIManager.Instance.MainPanel;
        go.GetComponentInChildren<TMP_Text>().text = quest.Title;
        go.GetComponent<QuestButton>().Quest = quest;
    }

    public void RemoveQuest(Quest quest)
    {
        foreach(Quest currentQuest in CurrentQuests)
        {
            if(currentQuest.Title == quest.Title)
            {
                CurrentQuests.Remove(currentQuest);
                break;
            }
        }

        foreach(Transform questTransform in UIManager.Instance.QuestList.transform)
        {
            if (questTransform.gameObject.GetComponent<QuestButton>().Quest.Title == quest.Title)
            {
                Destroy(questTransform.gameObject);
            }
        }
    }
}
