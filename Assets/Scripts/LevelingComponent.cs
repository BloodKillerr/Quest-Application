using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingComponent : MonoBehaviour
{
    public int CurrentXp = 0;
    public int DesiredXp = 100;
    public int CurrentLevel = 1;
    public int MaxLevel = 999;

    private void Start()
    {
        UpdateUI();
    }

    public void AddXp(int x)
    {
        CurrentXp += x;
        Check();
        UpdateUI();
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
            DesiredXp = DesiredXp + 150;
        }
        else
        {
            CurrentXp = DesiredXp;
            UIManager.Instance.LevelText.text = "Max";
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.Instance.LevelText.text = CurrentLevel.ToString();
        UIManager.Instance.XpSlider.maxValue = DesiredXp;
        UIManager.Instance.XpSlider.value = CurrentXp;
    }
}
