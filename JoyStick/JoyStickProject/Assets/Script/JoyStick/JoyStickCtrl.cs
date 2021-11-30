using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickCtrl : MonoBehaviour
{
    public float value;
    public Vector3 Direction;

    private void Awake()
    {
        
    }
    void Start()
    {
        value = 0;
    }

    private void Update()
    {
        JoyStickManager.Instance.Direction = Direction;
        JoyStickManager.Instance.Value = value;
    }
}
