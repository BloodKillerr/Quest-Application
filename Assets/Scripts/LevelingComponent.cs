using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingComponent : MonoBehaviour
{
    public int CurrentXp = 0;
    public int DesiredXp = 100;
    public int CurrentLevel = 1;
    public int MaxLevel = 999;

    public void AddXp(int x)
    {
        CurrentXp += x;
        Check();
    }

    public void RemoveXp(int x)
    {
        CurrentXp -= x;
    }

    public void Check()
    {
        while(CurrentXp>=DesiredXp)
        {
            LevelUp();
            RemoveXp(DesiredXp);
        }
    }

    public void LevelUp()
    {
        if(CurrentLevel != MaxLevel)
        {
            CurrentLevel += 1;
            DesiredXp = DesiredXp + 150;
        }
        else
        {
            CurrentXp = DesiredXp;
        }
    }
}
