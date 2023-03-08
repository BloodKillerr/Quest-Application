using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyManager : MonoBehaviour
{
    private void Start()
    {
        CheckTimers();
    }

    private void Update()
    {
        CheckTimers();
    }

    public void CheckTimers()
    {
        foreach(Quest quest in Player.Instance.QuestSystemComponent.CurrentQuests)
        {
            quest.CheckTime();
            quest.CheckIfToReset();
        }
    }
}
