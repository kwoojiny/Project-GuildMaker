using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  public void Set_Info(string json)
    {
        var data = JSON.Parse(json);

        FieldInfo[] FT = this.GetType().GetFields();

        foreach (KeyValuePair<string, JSONNode> kvp in (JSONObject)data)
        {
            FieldInfo Fi = this.GetType().GetField(kvp.Key);
            switch (kvp.Value.Tag)
            {
                case JSONNodeType.Number:
                    {
                        int this_data = kvp.Value;
                        Fi.SetValue(this, this_data);
                        break;
                    }
                case JSONNodeType.String:
                    {
                        string this_data = kvp.Value;
                        Fi.SetValue(this, this_data);
                        break;
                    }
            }
        }
    }
 * 
 * 
 */

public class Stat_Info {

    private int id;

    private int[] stats = new int[(int)GlobalVar.Stat_List.EXP];

    private List<int> personalities = new List<int>();

    private string name;

    public int[] Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    public List<int> Personalities
    {
        get
        {
            return personalities;
        }

        set
        {
            personalities = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}
