using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Loading_Sequence
{
    START = 0,
    CHECK_DATA_VERSION = 1,
    PATCH_DATA = 2,
    INIT_DATA = 3,
    INIT_ADVENDATA = 4,
    INIT_PARTYDATA = 5,
    END = 6,
}

public class GameManager : MonoBehaviour
{
    #region singleTon

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "GameManager";
                instance = container.AddComponent(typeof(GameManager)) as GameManager;
                container.transform.parent = GameObject.Find("Managers").transform;
            }

            return instance;
        }
    }
    #endregion

    private bool isgamestarted = false;

    private int game_day = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

        }

        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    }

    Loading_Sequence m_Loading = Loading_Sequence.START;
    bool isLoaded = false;

    public void Update()
    {
        if (isLoaded) return;

        Loading();
    }

    public void FixedUpdate()
    {
        if (!isgamestarted) return;


    }

    public void Loading()
    {
        switch(m_Loading)
        {
            case Loading_Sequence.START:
                {
                    m_Loading = Loading_Sequence.CHECK_DATA_VERSION;
                    break;
                }
            case Loading_Sequence.CHECK_DATA_VERSION:
                {
                    DataManager.Instance.Init();
                    break;
                }
            case Loading_Sequence.PATCH_DATA:
                {
                    break;
                }
            case Loading_Sequence.INIT_DATA:
                {

                    m_Loading = Loading_Sequence.INIT_ADVENDATA;
                    break;
                }

            case Loading_Sequence.INIT_ADVENDATA:
                {
                    AdventurerManager.Instance.Init();
                    m_Loading = Loading_Sequence.INIT_PARTYDATA;
                    break;
                }
            case Loading_Sequence.INIT_PARTYDATA:
                {
                    PartyManager.Instance.Init();
                    m_Loading = Loading_Sequence.END;
                    break;
                }
            case Loading_Sequence.END:
                {
                    SceneManager.LoadSceneAsync("2_Game", LoadSceneMode.Single);
                    SceneManager.LoadSceneAsync("1_Lobby", LoadSceneMode.Additive);
                    isLoaded = true;
                    break;
                }
        }
    }

    public void Complete_Get_Version(bool latestversion)
    {
        if (latestversion)
        {
            m_Loading = Loading_Sequence.INIT_DATA;
        } else
        {
            m_Loading = Loading_Sequence.PATCH_DATA;
            DataManager.GetData();
        }
    }


}
