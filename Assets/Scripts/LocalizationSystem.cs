using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public enum Language
    {
        English,
        Polish
    }

    public Language CurrentLanguage = Language.English;

    private Dictionary<string, string> localizedEN;
    private Dictionary<string, string> localizedPL;

    public bool isInit;

    #region Singleton
    public static LocalizationSystem Instance;

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

    public void ChangeLanguage(float index)
    {
        switch(index)
        {
            case 0:
                CurrentLanguage = Language.English;
                break;
            case 1:
                CurrentLanguage = Language.Polish;
                break;
        }

        foreach(TextLocalizerUI textUI in UIManager.Instance.TextLocalizerUIElements)
        {
            textUI.UpdateUI();
        }
    }

    public void Init()
    {
        CSVLoader.Instance.LoadCSV();

        localizedEN = CSVLoader.Instance.GetDictionaryValues("en");
        localizedPL = CSVLoader.Instance.GetDictionaryValues("pl");

        isInit = true;
    }

    public string GetLocalizedValue(string key)
    {
        if(!isInit)
        {
            Init();
        }

        string value = key;

        switch(CurrentLanguage)
        {
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
            case Language.Polish:
                localizedPL.TryGetValue(key, out value);
                break;
        }

        return value;
    }
}
