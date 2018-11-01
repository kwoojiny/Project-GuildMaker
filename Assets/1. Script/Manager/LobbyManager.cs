using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour {

    private static LobbyManager instance;

    public static LobbyManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "LobbyManager";
                instance = container.AddComponent(typeof(LobbyManager)) as LobbyManager;
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

        this.transform.parent = GameObject.Find("Managers").transform;
    }

    public void Create_Party()
    {
        PartyManager.Instance.Create_Party();
    }

    public void Create_Adventurer()
    {
        AdventurerManager.Instance.Create_New_Adventurer();
    }

    public void Open_Pub()
    {
        List<AdventurerInfo> Join_List = AdventurerManager.Instance.Join_List;

            foreach (var Adventurer in Join_List)
            {
                Debug.Log(Adventurer.Name);
            }
    }

}
