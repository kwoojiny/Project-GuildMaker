using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour {

    private static ClassManager instance;

    public static ClassManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject();
                container.name = "ClassManager";
                instance = container.AddComponent(typeof(ClassManager)) as ClassManager;
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

}
