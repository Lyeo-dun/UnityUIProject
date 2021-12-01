using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JoyStickMode
{
    Move, Rotate
};


public class JoyStickCtrl : MonoBehaviour
{
    public float value;
    public Vector3 Direction;

    [SerializeField] private JoyStickMode Mode;

    private void Awake()
    {
        
    }
    void Start()
    {
        value = 0;
    }

    private void Update()
    {
        if(Mode == JoyStickMode.Move)
        {
            JoyStickManager.Instance.MoveDirection = Direction;
            JoyStickManager.Instance.MoveValue = value;
        }
    }
}
