using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AdventurerManager : MonoBehaviour
{

    private List<AdventurerInfo> m_Adventurer_List = new List<AdventurerInfo>(); // 현재 사용할수있는 전체 모험가 리스트

    private List<AdventurerInfo> m_Join_List = new List<AdventurerInfo>(); // 가입대기중인 모험가 리스트

    private List<AdventurerInfo> m_Dead_List = new List<AdventurerInfo>(); // 죽은 모험가 리스트


    private static AdventurerManager instance;

    public static AdventurerManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "AdventurerManager";
                instance = container.AddComponent(typeof(AdventurerManager)) as AdventurerManager;
                container.transform.parent = GameObject.Find("Managers").transform;
            }

            return instance;
        }
    }

    public List<AdventurerInfo> Join_List
    {
        get { return m_Join_List; }

        set { m_Join_List = value; }
    }

    public List<AdventurerInfo> Dead_List
    {
        get { return m_Dead_List; }

        set { m_Dead_List = value; }
    }

    public List<AdventurerInfo> Adventurer_List
    {
        get { return m_Adventurer_List; }
        set { m_Adventurer_List = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Init()
    {
        Debug.Log(GetType().Name + " Initiate.");
    }

    public void Create_New_Adventurer()
    {
        AdventurerInfo Adventurer = new AdventurerInfo();

        Debug.Log(Adventurer.Name);

        Adventurer.Id = Adventurer_List.Count;

        Adventurer_List.Add(Adventurer);

        Debug.Log("모험가 생성 : " + Adventurer.Id);

        Join_List.Add(Adventurer);
    }

    public AdventurerInfo Find_Adventurer(int id)
    {
        AdventurerInfo Target = Adventurer_List.Find(x => x.Id == id);
        return Target;
    }

    public void Set_Party(int Adven_Id, int _Party_Id)
    {
        AdventurerInfo Target = Find_Adventurer(Adven_Id);
        Target.Party_Id = _Party_Id;

        PartyManager.Instance.Get_Party(_Party_Id).Party_Members.Add(Target);

        Debug.Log("파티 설정 완료");
    }

    public void New_Join_Adventurer()
    {

    }


}
