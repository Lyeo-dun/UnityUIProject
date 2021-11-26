using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    [SerializeField] private Vector3 StartPoint;
    [SerializeField] private Vector3 TargetPoint;

    [SerializeField] private float StartTime;

    [SerializeField] private bool isMove;
    [SerializeField] private bool isCheck;

    void Start()
    {
        isMove = false;
        isCheck = false;
    }

    void Update()
    {
        if(!isCheck)
        {
            StartTime = Time.time;
            isMove = true;

            StartPoint = this.transform.position;

            isCheck = true;
        }

        if(isMove)
        {
            float u = Time.time - StartTime;
            if(u > 1)
            {
                u = 1;
                isMove = false;
            }

            Debug.Log(u);

            u = EaseU(u);

            transform.position = (1 - u) * StartPoint + u * TargetPoint;
        }
    }

    private float EaseU(float u)
    {
        float u2 = u;
        u2 = u - 0.2f * Mathf.Sin(2.0f * Mathf.PI * u);

        return u2;
    }
}
