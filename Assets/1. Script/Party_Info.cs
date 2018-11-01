using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party_Info : MonoBehaviour {

    public int id;
    public List<AdventurerInfo> Party_Members;

    public Party_Info()
    {
        Party_Members = new List<AdventurerInfo>();
    }
}
