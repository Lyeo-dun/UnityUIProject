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
    private float SinValue;

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
                    SinValue = 0;
                }
            }
            
            if (ButtonsAni)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    //Buttons[i].GetComponent<RectTransform>().pivot += Vector2.left * Speed * 3.0f * Time.deltaTime;
                    Buttons[i].GetComponent<RectTransform>().pivot = Vector2.Lerp(Buttons[i].GetComponent<RectTransform>().pivot, (Vector2.right * 0.5f) +
                            (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y), 0.2f * Mathf.PI * Mathf.Sin(SinValue));

                    if (0.5f >= Buttons[i].GetComponent<RectTransform>().pivot.x)
                    {
                        Buttons[i].GetComponent<RectTransform>().pivot = (Vector2.right * 0.5f) +
                            (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y);

                        if (i == Buttons.Length - 1)
                        {
                            ButtonsAni = false;
                        }
                    }
                }
            }
        }
        else
        {
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

    // 포물선 만들기

    public void PopUpDraw()
    {
        PivotIndex = !PivotIndex;
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;

        UIManager.GetInstance.OpenSlide = PivotIndex;
    }
}
