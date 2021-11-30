using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickManager : MonoBehaviour
{
    static JoyStickManager _instance;
    static public JoyStickManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private JoyStickManager()
    { }

    public Vector3 Direction = Vector3.zero;
    public float Value = 0;

    private void Awake()
    {
        if (_instance == null)
            _instance = new JoyStickManager();
        else
        {
            if(_instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
