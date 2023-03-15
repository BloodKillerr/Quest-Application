using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Quest
{
    public string Title;

    public List<Requirement> Requirements = new List<Requirement>();

    public float PunishmentRate = 1f;

    public int XP = 50;

    public bool IsAvailable = true;

    public bool PunishmentApplied = false;

    public DateTime TimeToComplete;

    public bool IsComplete
    {
        get
        {
            foreach(Requirement requirement in Requirements)
            {
                if(!requirement.IsComplete)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public bool IsOnAnotherLevel
    {
        get
        {
            foreach (Requirement requirement in Requirements)
            {
                if (!requirement.IsOnAnotherLevel)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public void SetNewTime(System.DateTime dateTime)
    {
        IsAvailable = true;
        TimeToComplete = dateTime;
        UIManager.Instance.QuestCompleteButton.interactable = true;
        UIManager.Instance.QuestCompleteText.alpha = 1f;
    }

    public void GainRewards()
    {
        if(IsAvailable && IsComplete)
        {
            if (IsOnAnotherLevel)
            {
                Player.Instance.LevelingComponent.AddXp(XP * 2);
            }
            else
            {
                Player.Instance.LevelingComponent.AddXp(XP);
            }
            IsAvailable = false;
            UIManager.Instance.QuestCompleteButton.interactable = false;
            UIManager.Instance.QuestCompleteText.alpha = .6f;
        }   
    }

    public void CheckTime()
    {
        if(IsAvailable && !PunishmentApplied)
        {
            if(TimeToComplete < System.DateTime.Now)
            {
                ApplyPunishment();

                string newDate = string.Format("{0} 23:59:59", System.DateTime.Now.Date.ToString("dd-MM-yyyy"));

                SetNewTime(System.DateTime.ParseExact(newDate, "dd-MM-yyyy HH:mm:ss", null));

                foreach (Requirement requirement in Requirements)
                {
                    requirement.CurrentAmount = 0;
                    requirement.IsComplete = false;
                    requirement.IsOnAnotherLevel = false;
                }
            }
        }
        else if(IsAvailable && PunishmentApplied)
        {
            if (TimeToComplete < System.DateTime.Now)
            {
                string newDate = string.Format("{0} 23:59:59", System.DateTime.Now.Date.ToString("dd-MM-yyyy"));

                SetNewTime(System.DateTime.ParseExact(newDate, "dd-MM-yyyy HH:mm:ss", null));

                foreach (Requirement requirement in Requirements)
                {
                    requirement.CurrentAmount = 0;
                    requirement.IsComplete = false;
                    requirement.IsOnAnotherLevel = false;
                }
            }
        }
    }

    public void CheckIfToReset()
    {
        if(!IsAvailable)
        {
            if(TimeToComplete < System.DateTime.Now)
            {
                string newDate = string.Format("{0} 23:59:59", System.DateTime.Now.Date.ToString("dd-MM-yyyy"));

                SetNewTime(System.DateTime.ParseExact(newDate, "dd-MM-yyyy HH:mm:ss", null));

                foreach (Requirement requirement in Requirements)
                {
                    requirement.CurrentAmount = 0;
                    requirement.IsComplete = false;
                    requirement.IsOnAnotherLevel = false;
                }

                if(PunishmentApplied)
                {
                    RemovePunishment();
                }
            }
        }
    }

    public void ApplyPunishment()
    {
        foreach(Requirement requirement in Requirements)
        {
            requirement.DesiredAmount *= 2;
        }
        PunishmentApplied = true;
    }

    public void RemovePunishment()
    {
        foreach (Requirement requirement in Requirements)
        {
            requirement.DesiredAmount /= 2;
        }
        PunishmentApplied = false;
    }
}

[System.Serializable]
public class Requirement
{
    public string RequirementName;

    public int DesiredAmount = 100;

    public int CurrentAmount = 0;

    public bool IsComplete = false;

    public bool IsOnAnotherLevel = false;

    public Requirement(string requirementName, int desiredAmount)
    {
        RequirementName = requirementName;
        DesiredAmount = desiredAmount;
    }

    public void AddCount(int amount)
    {
        CurrentAmount += amount;

        if(CurrentAmount >= DesiredAmount)
        {
            IsComplete = true;
        }

        if(CurrentAmount >= DesiredAmount*2)
        {
            CurrentAmount = DesiredAmount * 2;
            IsOnAnotherLevel = true;
        }
    }

    public void RemoveCount(int amount)
    {
        CurrentAmount -= amount;

        if(CurrentAmount < 0)
        {
            CurrentAmount = 0;
        }

        if(CurrentAmount < DesiredAmount)
        {
            IsComplete = false;
        }

        if(CurrentAmount < DesiredAmount*2)
        {
            IsOnAnotherLevel = false;
        }
    }
}
