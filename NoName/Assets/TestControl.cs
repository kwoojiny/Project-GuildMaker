using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Enums;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

public class TestControl : MonoBehaviour {

	// Use this for initialization
	void Start () {

        var spriteCollection = FindObjectOfType<SpriteCollection>();

        if (spriteCollection == null) return;

        string name = SpriteCollection.Instance.Armor[0].Name;

        Debug.Log(name);

        this.GetComponent<Character>().EquipArmor(SpriteCollection.Instance.Armor[0].Sprites);

    }

    // Update is called once per frame
    void Update () {
	}
}
