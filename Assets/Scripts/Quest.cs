using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string Title;

    public Requirement[] Requirements;

    public float PunishmentRate = 1f;

    public int XP = 50;

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

    public void GainRewards()
    {
        Player.Instance.LevelingComponent.AddXp(XP);
    }
}

[System.Serializable]
public class Requirement
{
    public int DesiredAmount = 100;

    public int CurrentAmount = 0;

    public bool IsComplete = false;

    public bool IsOnAnotherLevel = false;

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
