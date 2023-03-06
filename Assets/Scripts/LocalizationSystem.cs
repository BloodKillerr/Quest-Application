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

    public string attributeId = "en";

    private Dictionary<string, string> localized;

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
                attributeId = "en";
                Init();
                break;
            case 1:
                CurrentLanguage = Language.Polish;
                attributeId = "pl";
                Init();
                break;
        }

        foreach(TextLocalizerUI textUI in UIManager.Instance.TextLocalizerUIElements)
        {
            textUI.UpdateUI();
        }
    }

    public void Init()
    {
        CSVLoader.Instance.LoadCSV(attributeId);

        localized = CSVLoader.Instance.GetDictionaryValues();

        isInit = true;
    }

    public string GetLocalizedValue(string key)
    {
        if(!isInit)
        {
            Init();
        }

        string value;

        localized.TryGetValue(key, out value);

        return value;
    }
}
