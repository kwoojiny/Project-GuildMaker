using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;


public enum TableName
{
    Null = -1,
    Version = 0,
    Quest = 1,
    Worker = 2,
}

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
            Debug.Log(reader.GetValue(0));

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

        //GetAllData(true);
        Version_Load();
    }

    public static void ParseData(CloudConnectorCore.QueryType query, List<string> objTypeNames, List<string> jsonData)
    {
        for (int i = 0; i < objTypeNames.Count; i++)
        {
            Debug.Log("Data type/table: " + objTypeNames[i]);
        }

        if (query == CloudConnectorCore.QueryType.getAllTables)
        {
            for (int i = 0; i < objTypeNames.Count; i++)
            {
                TableName Name = FindTable(objTypeNames[i]);
                switch (Name)
                {
                    case TableName.Version:
                        {
                            break;
                        }
                    case TableName.Quest:
                        {
                            break;
                        }
                }
            }
        }
        #region Version_Check
        else if (query == CloudConnectorCore.QueryType.getTable)
        {
            for (int i = 0; i < objTypeNames.Count; i++)
            {
                TableName Name = FindTable(objTypeNames[i]);
                switch(Name)
                {
                    case TableName.Version:
                        {
                            Version_Check(jsonData[i]);
                            break;
                        }
                }
            }
        }
        #endregion
    }

    public static TableName FindTable(string Name)
    {
        string[] Tables = System.Enum.GetNames(typeof(TableName));
        TableName Table = TableName.Null;
        for (int i = 0; i < Tables.Length; i++)
            if (Name.Equals(Tables[i]))
                Table = (TableName)System.Enum.Parse(typeof(TableName), Tables[i]);

        return Table;
    }

    // Get a player object from the cloud.
    public static void RetrieveGandalf(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving player of name Mithrandir from the Cloud.</color>");

        // Get any objects from table 'PlayerInfo' with value 'Mithrandir' in the field called 'name'.
        CloudConnectorCore.GetObjectsByField(tableName, "name", "Mithrandir", runtime);
    }

    // Get all player objects from the cloud.
    public static void GetAllData(bool runtime)
    {
        Debug.Log("<color=yellow>Retrieving all players from the Cloud.</color>");

        // Get all objects from table 'PlayerInfo'.
        CloudConnectorCore.GetAllTables(runtime);
    }

    public static void Version_Load()
    {
        CloudConnectorCore.GetTable("Version", true);
    }

    public static void Version_Check(string data)
    {
        var ArrayData = JSON.Parse(data);
        var Data = ArrayData[0];

        string Server_Version = Data["Version"];
        string Current_Version = PlayerPrefs.GetString("Version");
        if (Current_Version.Equals(Server_Version))
        {
            Debug.Log("TableData is up to date.");
            return;
        } else
        {
            PlayerPrefs.SetString("Version", Data["Version"]);
            Debug.Log("TableData Updated. Current Version : " + Data["Version"]);
        }

    }

}
