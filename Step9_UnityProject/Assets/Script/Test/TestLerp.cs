using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    [SerializeField] private Vector2 StartPoint;
    [SerializeField] private Vector2 TargetPoint;

    [SerializeField] private float StartTime;

    [SerializeField] private bool isMove;

    void Start()
    {
        isMove = false;
    }

    void Update()
    {
        if(isMove)
        {
            float u = (Time.time - StartTime) / 0.2f;
            if(u > 1)
            {
                u = 1;
                isMove = false;
            }

            u = EaseU(u);

            GetComponent<RectTransform>().pivot = (1 - u) * StartPoint + u * TargetPoint;
        }
    }
    public void SettingPoint(Vector2 start, Vector2 end)
    {
        StartPoint = start;
        TargetPoint = end;
    }

    public void ButtonStartPointSetting()
    {
        GetComponent<RectTransform>().pivot = StartPoint;
    }
    public void ButtonMoveSetting()
    {
        StartTime = Time.time;
        isMove = true;

        ButtonStartPointSetting();
    }
    private float EaseU(float u)
    {
        float u2 = u;
        u2 = u - 0.2f * Mathf.Sin(2.0f * Mathf.PI * u);

        return u2;
    }
}
