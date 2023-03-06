using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public QuestSystemComponent QuestSystemComponent;
    public LevelingComponent LevelingComponent;

    #region Singleton
    public static Player Instance;

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
}
