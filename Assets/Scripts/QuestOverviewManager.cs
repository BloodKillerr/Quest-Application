using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOverviewManager : MonoBehaviour
{
    public Quest Quest;
    public Transform RequirementsOverviewArea;
    public ChangePanel ChangePanel;
    #region Singleton
    public static QuestOverviewManager Instance;

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

    public void SetQuest(Quest quest)
    {
        Quest = quest;
        SetUI();
    }

    public void SetUI()
    {
        foreach(Transform t in RequirementsOverviewArea)
        {
            Destroy(t.gameObject);
        }

        UIManager.Instance.QuestTitleText.text = Quest.Title;
        UIManager.Instance.QuestRewardText.text = Quest.XP.ToString();
        UIManager.Instance.QuestPunishmentText.text = Quest.PunishmentRate.ToString();
        
        if(Quest.IsAvailable)
        {
            UIManager.Instance.QuestCompleteButton.interactable = true;
            UIManager.Instance.QuestCompleteText.alpha = 1f;
        }
        else
        {
            UIManager.Instance.QuestCompleteButton.interactable = false;
            UIManager.Instance.QuestCompleteText.alpha = .6f;
        }    

        foreach(Requirement requirement in Quest.Requirements)
        {
            GameObject go = Instantiate(UIManager.Instance.QuestOverviewRequirementPrefab, RequirementsOverviewArea);
            go.GetComponent<RequirementOverviewElement>().Requirement = requirement;
            go.GetComponent<RequirementOverviewElement>().UpdateUI();
        }
    }

    public void ClaimRewards()
    {
        if(Quest.IsComplete)
        {
            Quest.GainRewards();
            ChangePanel.ChangeCanvas();
            UIManager.Instance.AddToQueue("quest_completed");
        }   
    }

    public void RemoveQuest()
    {
        Player.Instance.QuestSystemComponent.RemoveQuest(Quest);
        ChangePanel.ChangeCanvas();
    }

    internal void UpdateUI()
    {
        if(Quest.IsOnAnotherLevel && !Quest.PunishmentApplied)
        {
            UIManager.Instance.QuestRewardText.text = (Quest.XP*2).ToString();
        }
        else
        {
            UIManager.Instance.QuestRewardText.text = Quest.XP.ToString();
        }
    }
}
