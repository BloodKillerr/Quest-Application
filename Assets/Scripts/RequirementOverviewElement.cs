using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementOverviewElement : MonoBehaviour
{
    public Requirement Requirement;
    public TMP_Text RequirementNameText;
    public Button IncreaseButton;
    public Button DecreaseButton;

    public void Increase()
    {
        Requirement.AddCount(1);
    }

    public void Decrease()
    {
        Requirement.RemoveCount(1);
    }

    public void UpdateUI()
    {
        RequirementNameText.text = string.Format("{0}, {1}/{2}", Requirement.RequirementName, Requirement.CurrentAmount, Requirement.DesiredAmount);
        QuestOverviewManager.Instance.UpdateUI();
    }
}
