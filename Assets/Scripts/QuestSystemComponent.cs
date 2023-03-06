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
        go.GetComponentInChildren<TMP_Text>().text = quest.Title;
    }
}
