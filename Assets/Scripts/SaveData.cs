using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public LevelingData MyLevelingData { get; set; }

    public List<QuestData> MyQuestData { get; set; }

    public LocalizationData MyLocalizationData { get; set; }

    public SaveData()
    {
        MyQuestData = new List<QuestData>();
    }
}

[System.Serializable]
public class LevelingData
{
    public int MyCurrentLevel { get; set; }

    public int MyCurrentXp { get; set; }

    public int MyMaxXp { get; set; }

    public LevelingData(LevelingComponent levelingComponent)
    {
        MyCurrentLevel = levelingComponent.CurrentLevel;
        MyCurrentXp = levelingComponent.CurrentXp;
        MyMaxXp = levelingComponent.DesiredXp;
    }
}

[System.Serializable]
public class QuestData
{
    public string MyTitle { get; set; }

    public List<Requirement> MyRequirements { get; set; }

    public int MyPunishmentRate { get; set; }

    public int MyXp { get; set; }

    public bool MyIsAvailable { get; set; }

    public bool MyPunishmentApplied { get; set; }

    public string MyTimeToComplete { get; set; }

    public QuestData(Quest quest)
    {
        MyTitle = quest.Title;
        MyRequirements = quest.Requirements;
        MyPunishmentRate = quest.PunishmentRate;
        MyXp = quest.XP;
        MyIsAvailable = quest.IsAvailable;
        MyPunishmentApplied = quest.PunishmentApplied;
        MyTimeToComplete = quest.TimeToComplete.ToString("dd-MM-yyyy HH:mm:ss");
    }
}

[System.Serializable]
public class LocalizationData
{
    public int MyCurrentLanguageId { get; set; }

    public LocalizationData(int myCurrentLanguageId)
    {
        MyCurrentLanguageId = myCurrentLanguageId;
    }
}
