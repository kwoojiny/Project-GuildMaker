using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;

public class DataManager : MonoBehaviour {

    private static string tableName;
    #region singleTon

    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                container.transform.parent = GameObject.Find("Managers").transform;
            }

            return instance;
        }
    }
    #endregion

    private bool isInit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        //Db_Init();
    }

    void Db_Init()
    {
        string filepath = "";
        filepath = Application.dataPath + "/Gamedb.db";
        if (!File.Exists(filepath))
        {
            File.Copy(Application.streamingAssetsPath + "/Gamedb.db", filepath);
            Debug.Log(Application.streamingAssetsPath + "/Gamedb.db");
        } else
        {
            Debug.Log(Application.streamingAssetsPath + "/Gamedb.db");
        }

        string conn = "URI=file:" + Application.dataPath + "/Gamedb.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT 1";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int value = reader.GetInt32(0);

            Debug.Log("value= " + value);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void Init()
    {
        if (isInit) return;

        isInit = true;

        Debug.Log(GetType().Name + " Initiate.");

        CloudConnectorCore.processedResponseCallback.AddListener(ParseData);

        GetVersion();
    }
    
    public static void ParseData(CloudConnectorCore.QueryType query, List<string> objTypeNames, List<string> jsonData)
    {
        /*
        for (int i = 0; i < objTypeNames.Count; i++)
        { 
            Debug.Log("Data type/table: " + query + " : " + objTypeNames[i]);
        }
        */
        switch(query)
        {
            case CloudConnectorCore.QueryType.getAllTables:
                {
                    Data_Loading(objTypeNames, jsonData);
                    break;
                }
            case CloudConnectorCore.QueryType.getTable:
                {

                    Version_Check(jsonData[0]);
                    break;
                }
        }
    }

    public static void Version_Check(string data)
    {
        var Json = JSON.Parse(data);
        string Next_Version = Json[0]["Version"];
        string Current_Version = PlayerPrefs.GetString("Version");
        Debug.Log(PlayerPrefs.GetString("Version"));

        if(Current_Version.Equals(Next_Version))
        {
            PlayerPrefs.SetString("Version", Json[0]["Version"]);
            GameManager.Instance.Complete_Get_Version(true);
        } else
        {
            Debug.Log("버전이 일치하지 않습니다.");
            GameManager.Instance.Complete_Get_Version(false);
        }
    }

    public static void Data_Loading(List<string> TableNames, List<string> jsonData)
    {
        for(int i = 0; i < TableNames.Count;i++)
        {
            if(TableNames[i].Equals("Version"))
            {
                var Json = JSON.Parse(jsonData[i]);
                PlayerPrefs.SetString("Version", Json[0]["Version"]);
            }



            //Debug.Log(TableNames[i] + ":" + jsonData[i]);
        }

        GameManager.Instance.Complete_Get_Version(true);
    }

    public static void GetVersion()
    {
        Debug.Log("<color=yellow>Retrieving Data Version from the Cloud.</color>");

        CloudConnectorCore.GetTable("Version", true);
    }

    public void Patch_Data()
    {
        GetData();
    }

    public static void GetData()
    {
        CloudConnectorCore.GetAllTables(true);
    }

}
