using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    private static PartyManager instance;

    public static PartyManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "PartyManager";
                instance = container.AddComponent(typeof(PartyManager)) as PartyManager;
                container.transform.parent = GameObject.Find("Managers").transform;
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

    public List<Party_Info> Party_List;

    public void Init()
    {
        Party_List = new List<Party_Info>();

        Debug.Log(GetType().Name + " Initiate.");
    }

    public void Create_Party()
    {
        int Party_Id = Party_List.Count;
        Party_Info Pi = new Party_Info();
        Pi.id = Party_Id;

        Party_List.Add(Pi);
        Debug.Log("파티 생성 : " + Pi.id);
    }

    public Party_Info Get_Party(int id)
    {
        return Party_List.Find(x => x.id == id);
    }
}
