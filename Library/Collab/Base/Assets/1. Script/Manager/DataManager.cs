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



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        Db_Init();
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
        Debug.Log(GetType().Name + " Initiate.");

        CloudConnectorCore.processedResponseCallback.AddListener(ParseData);

        GetAllPlayers(true);
    }
    
    public static void ParseData(CloudConnectorCore.QueryType query, List<string> objTypeNames, List<string> jsonData)
    {

        for (int i = 0; i < objTypeNames.Count; i++)
        {
            Debug.Log("Data type/table: " + objTypeNames[i]);
        }

        // First check the type of answer.
        if (query == CloudConnectorCore.QueryType.getAllTables)
        {
            // Just dump all content to the console, sorted by table name.
            for (int i = 0; i < objTypeNames.Count; i++)
            {


            }
        }
    }
    
    // Get a player object from the cloud.
    public static void RetrieveGandalf(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving player of name Mithrandir from the Cloud.</color>");

        // Get any objects from table 'PlayerInfo' with value 'Mithrandir' in the field called 'name'.
        CloudConnectorCore.GetObjectsByField(tableName, "name", "Mithrandir", runtime);
    }

    // Get all player objects from the cloud.
    public static void GetAllPlayers(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving all players from the Cloud.</color>");

        tableName = "TEST";

        // Get all objects from table 'PlayerInfo'.
        CloudConnectorCore.GetTable(tableName, runtime);
    }

}
