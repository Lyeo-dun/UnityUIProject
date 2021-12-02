using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            _instance = this;
        else
        {
            if(_instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        GameObject Enable = new GameObject("EnableList");
        Enable.transform.parent = GameObject.Find("Objectpool").transform;
        GameObject Disable = new GameObject("DisableList");
        Disable.transform.parent = GameObject.Find("Objectpool").transform;
    }
}
