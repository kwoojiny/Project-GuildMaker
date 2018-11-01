using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    private static QuestManager instance;

    public static QuestManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "QuestManager";
                instance = container.AddComponent(typeof(QuestManager)) as QuestManager;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Set_Data()
    {

    }
}
