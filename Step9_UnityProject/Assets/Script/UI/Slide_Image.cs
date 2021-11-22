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
        Speed = 0.8f;

        Pivots[0] = new Vector2(1.0f, 0.5f);
        Pivots[1] = new Vector2(0.0f, 0.5f);

        PivotIndex = false;

        Rect.pivot = Pivots[PivotIndex ?  1 : 0];
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;

        ButtonsAni = false;
    }

    private void FixedUpdate()
    {
        if(PivotIndex)
        {
            if (!ButtonsAni)
                for(int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].GetComponent<RectTransform>().pivot = (Vector2.right * (0.8f + (i * 0.2f))) + 
                        (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y);
                }

            if (PivotPoint < Rect.pivot.x) // 목표가 0이고 피벗이 1일때
            {
                Rect.pivot += Vector2.left * Speed * Time.deltaTime;

                if (PivotPoint >= Rect.pivot.x)
                {
                    Rect.pivot = (Vector2.left * PivotPoint) + (Vector2.up * Rect.pivot.y);
                    ButtonsAni = true;
                }
            }

            if(ButtonsAni)
            {
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].GetComponent<RectTransform>().pivot += (Vector2.left * 1.8f * Time.deltaTime);
                     
                    if (0.5f >= Buttons[i].GetComponent<RectTransform>().pivot.x)
                    {
                        Buttons[i].GetComponent<RectTransform>().pivot = (Vector2.right * 0.5f) +
                        (Vector2.up * Buttons[i].GetComponent<RectTransform>().pivot.y);
                    }
                }
            }
        }
        else
        {
            ButtonsAni = false;
            if (PivotPoint > Rect.pivot.x) // 목표가 1이고 피벗이 0일때
            {
                Rect.pivot += Vector2.right * Speed * Time.deltaTime;

                if (PivotPoint <= Rect.pivot.x)
                {
                    Rect.pivot = (Vector2.right * PivotPoint) + (Vector2.up * Rect.pivot.y);
                }
            }
        }
    }

    public void PopUpDraw()
    {
        PivotIndex = !PivotIndex;
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;
    }
}
