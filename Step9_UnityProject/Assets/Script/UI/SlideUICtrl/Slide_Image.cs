using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide_Image : MonoBehaviour
{
    private RectTransform Rect;
    private float Speed;

    [SerializeField] private Vector2[] Pivots = new Vector2[2];
    [SerializeField] private bool PivotIndex;
    [SerializeField] private float PivotPoint;

    [SerializeField] private GameObject[] Buttons;
    private bool ButtonsAni;
    private bool SinAni;
    private float tmp;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();

        Buttons = new GameObject[transform.GetChild(1).transform.childCount];

        for(int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i] = transform.GetChild(1).transform.GetChild(i).gameObject;
        }
    }
    private void Start()
    {
        Speed = 5.4f;

        Pivots[0] = new Vector2(1.0f, 0.5f);
        Pivots[1] = new Vector2(0.0f, 0.5f);

        PivotIndex = false;

        Rect.pivot = Pivots[PivotIndex ?  1 : 0];
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;

        ButtonsAni = false;
        SinAni = false;
        tmp = 0;

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<RectTransform>().pivot = (Vector2.right * (1.8f + (i * 1.2f))) +
                (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y);
        }
    }

    private void FixedUpdate()
    {
        if(PivotIndex)
        {
            if (PivotPoint < Rect.pivot.x) // 목표가 0이고 피벗이 1일때
            {
                Rect.pivot += Vector2.left * Speed * Time.deltaTime;

                if (PivotPoint >= Rect.pivot.x)
                {
                    Rect.pivot = (Vector2.left * PivotPoint) + (Vector2.up * Rect.pivot.y);
                    ButtonsAni = true;
                }
            }
            
            if (ButtonsAni)
            {
                float u = Time.time;
                u = (1 - u) * 0 + u * 1;
                u = EaseU(u);
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].GetComponent<RectTransform>().pivot = (1 - u) * Buttons[i].GetComponent<RectTransform>().pivot
                        + u * ((Vector2.right * 0.5f) +
                                (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y));

                    //Buttons[i].GetComponent<RectTransform>().pivot += Vector2.left * Speed * 3.0f * Time.deltaTime;

                    //if (0.2f >= Buttons[i].GetComponent<RectTransform>().pivot.x)
                    //{
                    //    Buttons[i].GetComponent<RectTransform>().pivot = (Vector2.right * 0.5f) +
                    //            (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y);

                    //    if (i == Buttons.Length - 1)
                    //    {
                    //        SinAni = true;
                    //        ButtonsAni = false;
                    //    }
                    //}
                }
            }
        }
        else
        {
            ButtonsAni = false;
            SinAni = false;
            if (PivotPoint > Rect.pivot.x) // 목표가 1이고 피벗이 0일때
            {
                Rect.pivot += Vector2.right * Speed * Time.deltaTime;

                if (PivotPoint <= Rect.pivot.x)
                {
                    Rect.pivot = (Vector2.right * PivotPoint) + (Vector2.up * Rect.pivot.y);

                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        Buttons[i].GetComponent<RectTransform>().pivot = (Vector2.right * (1.8f + (i * 1.2f))) +
                            (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y);
                    }
                }
            }
        }
    }

    private float EaseU(float u)
    {
        float u2 = u;
        u2 = u + 2.0f * Mathf.Sin(2 * Mathf.PI * u);

        return u2;
    }
    public void PopUpDraw()
    {
        PivotIndex = !PivotIndex;
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;

        UIManager.GetInstance.OpenSlide = PivotIndex;
    }
}
