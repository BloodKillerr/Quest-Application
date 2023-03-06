using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestButton : MonoBehaviour
{
    public Quest Quest;

    public void SwitchQuest()
    {
        QuestOverviewManager.Instance.SetQuest(Quest);
    }
}
