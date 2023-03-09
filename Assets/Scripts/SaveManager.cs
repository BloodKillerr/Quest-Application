using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("HasSaves"))
        {
            Load();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "QuestAppData.dat", FileMode.Create);

            SaveData data = new SaveData();

            SaveLevelingSystem(data);

            SaveQuests(data);

            SaveLocalization(data);

            bf.Serialize(file, data);

            file.Close();

            PlayerPrefs.SetInt("HasSaves", 1);
            PlayerPrefs.Save();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void SaveLevelingSystem(SaveData data)
    {
        data.MyLevelingData = new LevelingData(Player.Instance.LevelingComponent);
    }

    private void SaveQuests(SaveData data)
    {
        foreach(Quest quest in Player.Instance.QuestSystemComponent.CurrentQuests)
        {
            data.MyQuestData.Add(new QuestData(quest));
        }
    }

    private void SaveLocalization(SaveData data)
    {
        int languageIndex = LocalizationSystem.Instance.GetCurrentLanguageIndex();

        data.MyLocalizationData = new LocalizationData(languageIndex);
    }

    public void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "QuestAppData.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            LoadLevelingSystem(data);

            LoadQuests(data);

            LoadLocalization(data);

            file.Close();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void LoadLevelingSystem(SaveData data)
    {
        Player.Instance.LevelingComponent.CurrentLevel = data.MyLevelingData.MyCurrentLevel;
        Player.Instance.LevelingComponent.CurrentXp = data.MyLevelingData.MyCurrentXp;
        Player.Instance.LevelingComponent.DesiredXp = data.MyLevelingData.MyMaxXp;
        Player.Instance.LevelingComponent.UpdateUI();
    }

    private void LoadQuests(SaveData data)
    {
        foreach(QuestData questData in data.MyQuestData)
        {
            Quest quest = new Quest();
            quest.Title = questData.MyTitle;
            quest.Requirements = questData.MyRequirements;
            quest.PunishmentRate = questData.MyPunishmentRate;
            quest.XP = questData.MyXp;
            quest.IsAvailable = questData.MyIsAvailable;
            quest.PunishmentApplied = questData.MyPunishmentApplied;
            quest.TimeToComplete = System.DateTime.ParseExact(questData.MyTimeToComplete, "dd-MM-yyyy HH:mm:ss", null);

            Player.Instance.QuestSystemComponent.AddQuest(quest);
        }
    }

    private void LoadLocalization(SaveData data)
    {
        LocalizationSystem.Instance.ChangeLanguage(data.MyLocalizationData.MyCurrentLanguageId);

        foreach(TextLocalizerUI TLU in UIManager.Instance.TextLocalizerUIElements)
        {
            TLU.UpdateUI();
        }
    }
}
