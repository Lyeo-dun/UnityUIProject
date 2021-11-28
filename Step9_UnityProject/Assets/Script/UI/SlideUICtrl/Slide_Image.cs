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
        Speed = 5.4f;

        Pivots[0] = new Vector2(1.0f, 0.5f);
        Pivots[1] = new Vector2(0.0f, 0.5f);

        PivotIndex = false;

        Rect.pivot = Pivots[PivotIndex ?  1 : 0];
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;

        ButtonsAni = false;

        Vector2 ButtonStartPos = new Vector2(3.5f, 0.5f);
        Vector2 ButtonEndPos = new Vector2(0.5f, 0.5f);
        
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].GetComponent<TestLerp>() == null)
                Buttons[i].AddComponent<TestLerp>();

            Buttons[i].GetComponent<TestLerp>().SettingPoint(ButtonStartPos, ButtonEndPos);
            Buttons[i].GetComponent<TestLerp>().ButtonStartPointSetting();
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
                StartCoroutine("ButtonAni");
                ButtonsAni = false;
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
                        Buttons[i].GetComponent<TestLerp>().ButtonStartPointSetting();
                    }
                }
            }
        }
    }
    private float EaseU(float u)
    {
        float u2 = u;
        u2 = u - 0.2f * Mathf.Sin(2 * Mathf.PI * u);

        return u2;
    }

    private IEnumerator ButtonAni()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            yield return new WaitForSeconds(0.02f);

            Buttons[i].GetComponent<TestLerp>().ButtonMoveSetting();
        }
    }
    public void PopUpDraw()
    {
        PivotIndex = !PivotIndex;
        PivotPoint = Pivots[PivotIndex ? 1 : 0].x;

        UIManager.GetInstance.OpenSlide = PivotIndex;
    }
}
