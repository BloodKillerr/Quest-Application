using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestCreationManager : MonoBehaviour
{
    [Header("QuestUI")]
    public TMP_InputField QuestTitleInputField;
    public TMP_InputField RequirementsNumberInputField;
    public TMP_InputField ExperienceAmountInputField;
    public TMP_InputField PunishmentRateInputField;

    [Header("Requirements")]
    public Transform RequirementsArea;
    public GameObject RequirementPrefab;

    public void CreateRequirements()
    {
        int numberOfRequirements;
        int.TryParse(RequirementsNumberInputField.text, out numberOfRequirements);

        foreach(Transform requirement in RequirementsArea)
        {
            Destroy(requirement.gameObject);
        }

        for (int i=0; i<numberOfRequirements; i++)
        {
            Instantiate(RequirementPrefab, RequirementsArea);
        }
    }

    public void SaveQuest()
    {
        Quest quest = new Quest();
        quest.Title = QuestTitleInputField.text;
        int.TryParse(ExperienceAmountInputField.text, out quest.XP);
        float.TryParse(PunishmentRateInputField.text, out quest.PunishmentRate);
        int numberOfRequirements;
        int.TryParse(RequirementsNumberInputField.text, out numberOfRequirements);

        if (quest.Title == string.Empty || quest.XP == 0 || quest.PunishmentRate == 0f)
        {
            return;
        }

        if (numberOfRequirements != RequirementsArea.childCount)
        {
            return;
        }

        for (int i=0; i<numberOfRequirements; i++)
        {
            Transform requirement = RequirementsArea.GetChild(i);
            TMP_InputField[] fields = requirement.GetComponentsInChildren<TMP_InputField>();

            if (fields[0].text == string.Empty || fields[1].text == string.Empty)
            {
                return;
            }

            quest.Requirements.Add(new Requirement(fields[0].text, int.Parse(fields[1].text)));
        }

        if(quest.Requirements.Count == 0)
        {
            return;
        }

        string exactTime = string.Format("{0} 23:59:59", System.DateTime.Now.Date.ToString("dd-MM-yyyy"));

        quest.SetTimeToComplete(System.DateTime.ParseExact(exactTime, "dd-MM-yyyy HH:mm:ss", null));

        Player.Instance.QuestSystemComponent.AddQuest(quest);
        ClearCreationPanel();
    }

    public void ClearCreationPanel()
    {
        QuestTitleInputField.text = string.Empty;
        RequirementsNumberInputField.text = string.Empty;
        ExperienceAmountInputField.text = string.Empty;
        PunishmentRateInputField.text = string.Empty;

        foreach (Transform requirement in RequirementsArea)
        {
            Destroy(requirement.gameObject);
        }
    }
}
