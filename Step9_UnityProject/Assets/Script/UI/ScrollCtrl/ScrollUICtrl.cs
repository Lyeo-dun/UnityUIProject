using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUICtrl : MonoBehaviour
{
    private GameObject ScrollView;
    private RectTransform ScrollViewUIRect;
    [SerializeField] private RectTransform[] ScrollViewContents;

    private bool isOpen;
    private float Speed;

    private bool isButtonAni;

    private void Awake()
    {
        ScrollView = gameObject.transform.GetChild(0).gameObject;
        ScrollViewUIRect = ScrollView.GetComponent<RectTransform>();

        ScrollViewContents = new RectTransform[ScrollView.transform.GetChild(1).transform.GetChild(0).transform.childCount];
    }

    private void Start()
    {
        for(int i = 0; i < ScrollViewContents.Length; i++)
        {
            ScrollViewContents[i] = ScrollView.transform.GetChild(1).transform.GetChild(0).transform.
                GetChild(i).GetComponent<RectTransform>();

            ScrollViewContents[i].anchoredPosition = new Vector2(-120.0f, (-30.0f * i));
        }

        ScrollViewUIRect.anchoredPosition = new Vector2(0.0f, 0.0f);

        isOpen = false;
        Speed = 800.0f;
    }

    public void ClickButton()
    {
        isOpen = !isOpen;
        UIManager.GetInstance.OpenScroll = isOpen;
    }

    public void FixedUpdate()
    {
        if(isOpen)
        {
            if (ScrollViewUIRect.anchoredPosition.y > -600.0f)
            {
                ScrollViewUIRect.anchoredPosition += Vector2.down * Speed * Time.deltaTime;

                if(ScrollViewUIRect.anchoredPosition.y <= -600.0f)
                {
                    ScrollViewUIRect.anchoredPosition = Vector2.down * 600.0f;
                    isButtonAni = true;
                }
            }

            if(isButtonAni)
            {
                for(int i = 0; i < ScrollViewContents.Length; i++)
                {
                    if(ScrollViewContents[i].anchoredPosition.y > -1 * (80 + (80 * i)))
                    {
                        ScrollViewContents[i].anchoredPosition += Vector2.down * Speed * Time.deltaTime;
                        
                        if(ScrollViewContents[i].anchoredPosition.y <= -1 * (80 + (80 * i)))
                        {
                            ScrollViewContents[i].anchoredPosition = (Vector2.left * 120) + (Vector2.down * (80 + (80 * i)));
                            if(i == ScrollViewContents.Length - 1)
                            {
                                isButtonAni = false;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (ScrollViewUIRect.anchoredPosition.y < 0)
            {
                ScrollViewUIRect.anchoredPosition += Vector2.up * Speed * Time.deltaTime;

                if(ScrollViewUIRect.anchoredPosition.y >= 0)
                {
                    for (int i = 0; i < ScrollViewContents.Length; i++)
                    {
                        ScrollViewContents[i].anchoredPosition = Vector2.left * 120 + (Vector2.down * (30.0f * i));
                    }
                }
            }

            isButtonAni = false;
        }
    }
}
