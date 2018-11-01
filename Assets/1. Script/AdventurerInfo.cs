using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerInfo : Stat_Info {

    public int Party_Id;

    public AdventurerInfo()
    {
        this.Name = "";
        this.Party_Id = -1;

        Name_Generate();
    }

    public void Name_Generate()
    {
        int Count = Random.Range(0, 10);

        string _Name = "Test" + Count;

        this.Name = _Name;
    }

}
