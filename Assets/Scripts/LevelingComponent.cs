using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingComponent : MonoBehaviour
{
    public int CurrentXp = 0;
    public int DesiredXp = 100;
    public int CurrentLevel = 1;
    public int MaxLevel = 99;

    private void Start()
    {
        UpdateUI();
    }

    public void AddXp(int x)
    {
        if(CurrentLevel != MaxLevel)
        {
            CurrentXp += x;
            Check();
            UpdateUI();
        }     
    }

    public void RemoveXp(int x)
    {
        CurrentXp -= x;
        UpdateUI();
    }

    public void Check()
    {
        while(CurrentXp>=DesiredXp)
        {
            RemoveXp(DesiredXp);
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if(CurrentLevel != MaxLevel)
        {
            CurrentLevel += 1;
            UIManager.Instance.AddToQueue("levelup");
            DesiredXp = DesiredXp + 500;
        }
        else
        {
            CurrentXp = DesiredXp;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(CurrentLevel != MaxLevel)
        {
            UIManager.Instance.LevelText.text = CurrentLevel.ToString();
        }
        else
        {
            UIManager.Instance.LevelText.text = "Max";
        }
        UIManager.Instance.XpSlider.maxValue = DesiredXp;
        UIManager.Instance.XpSlider.value = CurrentXp;
    }
}
